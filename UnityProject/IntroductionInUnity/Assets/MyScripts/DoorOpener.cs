using UnityEngine;


namespace EscapeRoom {

    public class DoorOpener : Door {

        #region UnityMethods

        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                Debug.Log(collider.name);
                OpenDoor();
            }
        }

        #endregion

    }
}
