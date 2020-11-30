

namespace EscapeRoom {

    public class StateMachine {

        #region Properties

        public State CurrentState {
            get;
            private set;
        }

        #endregion


        #region Methods

        public void Initialize(State startingState) {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState) {
            CurrentState.Exit();

            CurrentState = newState;
            CurrentState.Enter();
        }

        #endregion
    }
}
