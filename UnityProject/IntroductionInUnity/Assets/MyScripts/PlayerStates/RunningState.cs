using UnityEngine;


namespace EscapeRoom {

    public class RunningState : GroundedState {

        #region Fields

        private bool _isHoldingRun;
        private bool _isJumping;

        private const string _animationKey = "isRunning";

        #endregion


        #region ClassLifeCycles

        public RunningState(Player player, StateMachine stateMachine) : base(player, stateMachine, _animationKey) { }

        #endregion


        #region Methods

        public override void Enter() {
            base.Enter();

            _isJumping = false;
            _speed = _player.RunSpeed;
            _rotationSpeed = _player.RunRotationSpeed;
        }

        public override void Exit() {
            base.Exit();

            _player.SetAnimatorBool(_animationKey, false);
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

