using UnityEngine;


namespace EscapeRoom {

    public class FireExtinguisher : MonoBehaviour, IPickable {

        #region Fields

        [SerializeField] private GameObject _fireExtinguisher;

        private const PickableTypes _pickableType = PickableTypes.FireExtinguisher;

        #endregion


        #region Methods

        public PickableTypes OnPick() {
            _fireExtinguisher.SetActive(false);

            return _pickableType;
        }

        #endregion
    }
}
