using UnityEngine;


namespace EscapeRoom {

    public class GroundedState : State {

        #region Fields

        protected float _speed;
        protected float _rotationSpeed;

        private string _animationKey;

        private float _horizontalInput;
        private float _verticalInput;

        private bool _isThrowingScrap;
        private bool _isLayingLandmine;

        #endregion


        #region ClassLifeCycles

        public GroundedState(Player player, StateMachine stateMachine, string animationKey) : base(player, stateMachine) {
            _animationKey = animationKey;
        }

        #endregion


        #region Methods

        public override void Enter() {
            base.Enter();

            _horizontalInput = _verticalInput = 0.0f;
        }

        public override void Exit() {
            base.Exit();

            _player.ResetMoveParams();
        }

        public override void HandleInput() {
            base.HandleInput();

            _verticalInput = Input.GetAxis("Vertical");
            _horizontalInput = Input.GetAxis("Horizontal");

            _isThrowingScrap = Input.GetButtonDown("Fire1");
            _isLayingLandmine = Input.GetButtonDown("E");
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            bool _isMovement = !Mathf.Approximately(_verticalInput, 0.0f) || !Mathf.Approximately(_horizontalInput, 0.0f);

            if (_isMovement) {
                _player.Move(_verticalInput * _speed, _horizontalInput * _rotationSpeed);
            }

            if (_isThrowingScrap) {
                _player.ThrowScrap();
            }

            if (_isLayingLandmine) {
                _player.LayLandmine();
            }

            _player.SetAnimatorBool(_animationKey, _isMovement);
        }

        #endregion
    }
}
