using UnityEngine;


namespace EscapeRoom {

    public class RunningState : GroundedState {

        #region Fields

        private bool _isHoldingRun;
        private bool _isJumping;

        #endregion


        #region ClassLifeCycles

        public RunningState(Player player, StateMachine stateMachine) : base(player, stateMachine) { }

        #endregion


        #region Methods

        public override void Enter() {
            base.Enter();

            _isJumping = false;
            _speed = _player.RunSpeed;
            _rotationSpeed = _player.RunRotationSpeed;
        }

        public override void HandleInput() {
            base.HandleInput();

            _isJumping = Input.GetButton("Jump");
            _isHoldingRun = Input.GetButton("Fire2");
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (_isJumping) {
                _stateMachine.ChangeState(_player.RunJumping);
            } else if (!_isHoldingRun) {
                _stateMachine.ChangeState(_player.Standing);
            }
        }

        #endregion
    }
}

