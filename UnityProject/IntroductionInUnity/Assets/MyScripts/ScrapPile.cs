﻿using UnityEngine;


namespace EscapeRoom {

    public class ScrapPile : MonoBehaviour, IPickable {

        #region Fields

        private const PickableTypes _pickableType = PickableTypes.ScrapPile;

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

