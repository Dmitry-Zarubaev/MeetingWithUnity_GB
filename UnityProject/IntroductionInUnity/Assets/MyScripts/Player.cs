﻿using UnityEngine;


namespace EscapeRoom {

    [RequireComponent(typeof(CapsuleCollider))] public class Player : MonoBehaviour {

        #region Fields

        private StateMachine _stateMachine;

        public StandingState Standing;
        public DuckingState Ducking;
        public RunningState Running;
        public JumpingState Jumping;
        public JumpingState RunJumping;

        [SerializeField] private PlayerData _playerData;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private float collisionOverlapRadius = 0.1f;

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
    
        public void Move(float speed, float rotationSpeed) {
            Vector3 targetVelocity = speed * transform.forward * Time.deltaTime;
            targetVelocity.y = GetComponent<Rigidbody>().velocity.y;

            GetComponent<Rigidbody>().velocity = targetVelocity;
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

        #endregion
    }
}
