using UnityEngine;


namespace EscapeRoom {

    public class DuckingState : GroundedState {

        #region Fields

        private bool _isBelowCeiling;
        private bool _isHoldingCrouch;

        #endregion


        #region ClassLifeCycles

        public DuckingState(Player player, StateMachine stateMachine) : base(player, stateMachine) {}

        #endregion


        #region Methods

        public override void Enter() {
            base.Enter();

            _speed = _player.CrouchSpeed;
            _rotationSpeed = _player.CrouchRotationSpeed;
            _player.ColliderSize = _player.CrouchColliderHeight;
            _isBelowCeiling = false;
        }

        public override void Exit() {
            base.Exit();

            _player.ColliderSize = _player.NormalColliderHeight;
        }

        public override void HandleInput() {
            base.HandleInput();

            _isHoldingCrouch = Input.GetButton("Fire3");
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (!(_isHoldingCrouch || _isBelowCeiling)) {
                _stateMachine.ChangeState(_player.Standing);
            }
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            _isBelowCeiling = _player.CheckCollisionOverlap(_player.transform.position + Vector3.up * _player.NormalColliderHeight);
        }

        #endregion
    }
}

