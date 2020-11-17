using UnityEngine;


namespace EscapeRoom {

    public class Fire : MonoBehaviour {

        private CapsuleCollider _collider;

        private void Start() {
            _collider = GetComponent<CapsuleCollider>();
        }
    }
}

