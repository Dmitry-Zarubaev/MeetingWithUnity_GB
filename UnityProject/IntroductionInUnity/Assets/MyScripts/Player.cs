using UnityEngine;


namespace EscapeRoom {

    [RequireComponent(typeof(CapsuleCollider))] public class Player : MonoBehaviour {

        #region Fields

        public StandingState Standing;
        public DuckingState Ducking;
        public RunningState Running;
        public JumpingState Jumping;
        public JumpingState RunJumping;

        [SerializeField] private PlayerData _playerData;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private float collisionOverlapRadius = 0.1f;

        private Vector3 _movement;
        private Animator _animator;
        private Rigidbody _body;
        private StateMachine _stateMachine;

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

        #endregion


        #region UnityMethods

        private void Start() {
            _stateMachine = new StateMachine();
            _animator = GetComponent<Animator>();
            _body = GetComponent<Rigidbody>();

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

        #endregion
    }
}
