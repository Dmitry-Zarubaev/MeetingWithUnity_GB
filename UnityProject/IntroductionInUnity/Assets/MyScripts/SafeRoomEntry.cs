using UnityEngine;


namespace EscapeRoom {

    public class SafeRoomEntry : MonoBehaviour {

        #region Fields

        [SerializeField] private SelfDestractionActivator _selfDestructionActivator;

        private UserInterface _uiController;

        #endregion


        #region UnityMethods

        private void Start() {
            _uiController = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UserInterface>();
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.CompareTag("Player")) {
                _selfDestructionActivator.DeactivateSelfDestruction();
                _uiController.SetActiveCountdown(false);
                _uiController.SetGameOver(true);
            }
        }

        #endregion
    }
}
