using UnityEngine;


namespace EscapeRoom {

    public class SafeRoomEntry : MonoBehaviour {

        #region Fields

        [SerializeField] private GameObject _userInterface;
        private UserInterface _uiController;

        #endregion


        #region UnityMethods

        private void Start() {
            _uiController = _userInterface.GetComponent<UserInterface>();
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.CompareTag("Player")) {
                _uiController.SetGameOver(true);
            }
        }

        #endregion
    }
}
