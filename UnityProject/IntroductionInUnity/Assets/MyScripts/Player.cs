using UnityEngine;
using UnityEngine.UI;


namespace EscapeRoom {

    [RequireComponent(typeof(CapsuleCollider))] public class Player : MonoBehaviour, IDamageable {

        #region Fields

        public StandingState Standing;
        public DuckingState Ducking;
        public RunningState Running;
        public JumpingState Jumping;
        public JumpingState RunJumping;

        public bool IsFireExtinguisherEquiped = false;

        [SerializeField] private PlayerData _playerData;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private GameObject _userInterface;
        [SerializeField] private GameObject _fireExtinguisher;
        [SerializeField] private float collisionOverlapRadius = 0.1f;

        private Vector3 _movement;
        private Animator _animator;
        private Rigidbody _body;
        private StateMachine _stateMachine;
        private UserInterface _uiController;

        private float _health;
        private int _scrapCounter = 0;

        #endregion


        #region Properties

        public float CollisionOverlapRadius => collisionOverlapRadius;

        public float NormalColliderHeight => _playerData.NormalColliderHeight;
        public float CrouchColliderHeight => _playerData.CrouchColliderHeight;

        public float CrouchRotationSpeed => _playerData.CrouchRotationSpeed;
        public float RunRotationSpeed => _playerData.RunRotationSpeed;
        public float RotationSpeed => _playerData.RotationSpeed;

        public float MovementSpeed => _playerData.MovementSpeed;
        public float CrouchSpeed => _playerData.CrouchSpeed;
        public float RunSpeed => _playerData.RunSpeed;

        public float RunJumpForce => _playerData.RunJumpForce;
        public float JumpForce => _playerData.JumpForce;

        public float MaxHealth => _playerData.MaxHealth;

        public float ColliderSize {
            get => GetComponent<CapsuleCollider>().height;

            set {
                GetComponent<CapsuleCollider>().height = value;
                Vector3 center = GetComponent<CapsuleCollider>().center;
                center.y = value / 2.0f;
                GetComponent<CapsuleCollider>().center = center;
            }
        }

        #endregion


        #region Methods

        public void SetAnimatorBool(string key, bool value) {
            _animator.SetBool(key, value);
        }
    
        public void Move(float speed, float rotationSpeed) {
            _movement = speed * transform.forward * Time.deltaTime;
            _movement.y = GetComponent<Rigidbody>().velocity.y;

            GetComponent<Rigidbody>().velocity = _movement;
            GetComponent<Rigidbody>().angularVelocity = rotationSpeed * Vector3.up * Time.deltaTime;
        }

        public void ResetMoveParams() {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        public void ApplyImpulse(Vector3 force) {
            GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

        public bool CheckCollisionOverlap(Vector3 point) {
            return Physics.OverlapSphere(point, CollisionOverlapRadius, _groundLayer).Length > 0;
        }

        public void TakeDamage(float damage) {
            _health -= damage;

            if (_health <= 0.0f) {
                Die();
                _health = 0.0f;
            }

            _uiController.SetHealthBar(_health / MaxHealth);
        }

        public void HealDamage(float heal) {
            _health += heal;

            if (_health > MaxHealth) {
                _health = MaxHealth;
            }
        }

        private void Die() {
            _uiController.SetGameOver(false);
            Destroy(this);
        }

        private void PickItem(IPickable pickable) {
            switch (pickable.OnPick()) {
                case PickableTypes.FireExtinguisher:
                    _uiController.SetInventory("Fire Extinguisher");
                    _fireExtinguisher.SetActive(true);
                    IsFireExtinguisherEquiped = true;
                    break;
                case PickableTypes.ScrapPile:
                    
                    break;
                default:
                    break;
            }
        }

        #endregion


        #region UnityMethods

        private void Start() {
            _health = MaxHealth;
            _stateMachine = new StateMachine();
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody>();
            _uiController = _userInterface.GetComponent<UserInterface>();

            Standing = new StandingState(this, _stateMachine);
            Ducking = new DuckingState(this, _stateMachine);
            Running = new RunningState(this, _stateMachine);
            Jumping = new JumpingState(this, _stateMachine, JumpForce);
            RunJumping = new JumpingState(this, _stateMachine, RunJumpForce);

            _stateMachine.Initialize(Standing);
        }

        private void Update() {
            _stateMachine.CurrentState.HandleInput();

            _stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate() {
            _stateMachine.CurrentState.PhysicsUpdate();
        }

        private void OnAnimatorMove() {
            _body.MovePosition(_body.position + _movement * _animator.deltaPosition.magnitude);
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.CompareTag("Pickable")) {
                IPickable pickable;
                collider.TryGetComponent<IPickable>(out pickable);

                if (pickable != null) {
                    PickItem(pickable);
                }
            }

            if (collider.CompareTag("Fire") && IsFireExtinguisherEquiped) {
                Fire fire = collider.GetComponent<Fire>();
                fire?.PutOutFire();
                _fireExtinguisher.SetActive(false);
                _uiController.SetInventory("-- empty --");
            }
        }

        #endregion
    }
}
