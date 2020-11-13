using UnityEngine;


namespace EscapeRoom {

    public class StandingState : GroundedState {

        #region Fields

        private bool _isJumping;
        private bool _isRunning;
        private bool _isCrouching;

        #endregion


        #region ClassLifeCycles

        public StandingState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

        #endregion


        #region Methods

        public override void Enter() {
            base.Enter();

            _speed = _player.MovementSpeed;
            _rotationSpeed = _player.RotationSpeed;
            _isCrouching = false;
            _isJumping = false;
            _isRunning = false;
        }

        public override void HandleInput() {
            base.HandleInput();

            _isCrouching = Input.GetButtonDown("Fire3");
            _isRunning = Input.GetButtonDown("Fire2");
            _isJumping = Input.GetButtonDown("Jump");
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (_isCrouching) {
                _stateMachine.ChangeState(_player.Ducking);
            } else if (_isJumping) {
                _stateMachine.ChangeState(_player.Jumping);
            } else if (_isRunning) {
                _stateMachine.ChangeState(_player.Running);
            }
        }

        #endregion
    }
}
