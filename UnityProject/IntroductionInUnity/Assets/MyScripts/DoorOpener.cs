using UnityEngine;


namespace EscapeRoom {

    public class DoorOpener : Door {

        #region Fields

        [SerializeField] private MeshRenderer _marker;
        [SerializeField] private Material _diactive;
        [SerializeField] private Material _active;

        #endregion


        #region UnityMethods

        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                Debug.Log(collider.name);
                _marker.material = _active;
                OpenDoor();
            }
        }

        #endregion
    }
}
