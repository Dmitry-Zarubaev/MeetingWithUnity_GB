using UnityEngine;


namespace EscapeRoom {

    public class DoorCloser : Door {

        #region Fields

        [SerializeField] private Animator _crazyDoor;
        [SerializeField] private GameObject _countdownUI;

        #endregion


        #region Methods

        private void ActivateSelfDestruction() {
            _crazyDoor.SetBool("isAnimated", true);
            _countdownUI.SetActive(true);
        }

        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                Debug.Log(collider.name);

                CloseDoor();
                ActivateSelfDestruction();
            }
        }

        #endregion
    }
}
