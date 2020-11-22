using UnityEngine;


namespace EscapeRoom {

    public class Door : MonoBehaviour {

        #region Fields

        [SerializeField] protected Animator _door;

        #endregion


        #region Methods

        protected void OpenDoor() {
            _door.SetBool("isOpening", true);
            _door.SetBool("isClosing", false);
        }

        protected void CloseDoor() {
            _door.SetBool("isOpening", false);
            _door.SetBool("isClosing", true);
        }

        #endregion
    }
}
