using UnityEngine;


namespace EscapeRoom {

    public class Camera : MonoBehaviour {

        #region Fields

        [SerializeField] private Transform target = null;

        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private float smoothTime = 5.0f;

        private Vector3 currentVelocity;

        #endregion


        #region Methods

        private void LateUpdate() {
            transform.LookAt(target);
            transform.position = Vector3.SmoothDamp(transform.position, offset + target.position, ref currentVelocity, smoothTime);
        }

        #endregion
    }
}