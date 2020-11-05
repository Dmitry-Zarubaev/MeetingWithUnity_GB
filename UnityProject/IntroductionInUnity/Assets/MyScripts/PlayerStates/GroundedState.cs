using UnityEngine;


namespace EscapeRoom {

    public class GroundedState : State {

        #region Fields

        protected float _speed;
        protected float _rotationSpeed;

        private float _horizontalInput;
        private float _verticalInput;

        #endregion


        #region ClassLifeCycles

        public GroundedState(Player player, StateMachine stateMachine) : base(player, stateMachine) {}

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
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            _player.Move(_verticalInput * _speed, _horizontalInput * _rotationSpeed);
        }

        #endregion
    }
}
