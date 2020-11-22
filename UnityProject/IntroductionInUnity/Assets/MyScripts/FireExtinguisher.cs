using UnityEngine;


namespace EscapeRoom {

    public class FireExtinguisher : MonoBehaviour, IPickable {

        #region Fields

        private const PickableTypes _pickableType = PickableTypes.FireExtinguisher;

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
