using UnityEngine;


namespace EscapeRoom {

    public class LandminePile : MonoBehaviour, IPickable {

        #region Fields

        private const PickableTypes _pickableType = PickableTypes.LandminePile;

        #endregion


        #region Methods

        public PickableTypes OnPick() {
            gameObject.SetActive(false);
            Destroy(this);

            return _pickableType;
        }

        #endregion
    }
}

