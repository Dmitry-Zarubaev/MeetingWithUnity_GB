using UnityEngine;


namespace EscapeRoom {

    public abstract class State {

        #region Fields

        protected Player _player;
        protected StateMachine _stateMachine;

        #endregion


        #region ClassLifeCycles

        protected State(Player player, StateMachine stateMachine) {
            _player = player;
            _stateMachine = stateMachine;
        }

        #endregion


        #region Methods

        public virtual void Enter() {
            Debug.Log(this.GetType().Name);
        }

        public virtual void HandleInput() {

        }

        public virtual void LogicUpdate() {

        }

        public virtual void PhysicsUpdate() {

        }

        public virtual void Exit() {

        }

        #endregion
    }
}
