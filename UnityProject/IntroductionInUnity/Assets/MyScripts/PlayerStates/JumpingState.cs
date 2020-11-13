using UnityEngine;


namespace EscapeRoom {

    public class JumpingState : State {

        #region Fields

        private float _jumpForce;
        private bool _isJumping;

        #endregion


        #region ClassLifeCycles

        public JumpingState(Player player, StateMachine stateMachine, float jumpForce) : base(player, stateMachine) {
            _jumpForce = jumpForce;
        }

        #endregion


        #region Methods

        private void Jump() {
            _player.transform.Translate(Vector3.up * (_player.CollisionOverlapRadius + 0.1f));
            _player.ApplyImpulse(Vector3.up * _jumpForce);
        }

        public override void Enter() {
            base.Enter();

            _isJumping = false;
            Jump();
        }

        public override void LogicUpdate() {
            base.LogicUpdate();

            if (_isJumping) {
                _stateMachine.ChangeState(_player.Standing);
            }
        }

        public override void PhysicsUpdate() {
            base.PhysicsUpdate();

            _isJumping = _player.CheckCollisionOverlap(_player.transform.position);
        }

        #endregion
    }
}
