using UnityEngine;


namespace EscapeRoom {

    public interface IPickable {

        #region Methods

        PickableTypes OnPick();

        #endregion
    }
}
