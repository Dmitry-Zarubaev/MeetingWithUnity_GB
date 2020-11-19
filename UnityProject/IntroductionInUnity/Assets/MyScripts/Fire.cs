using UnityEngine;


namespace EscapeRoom {

    public class Fire : MonoBehaviour {

        #region Fields

        [SerializeField] private float _damagePerSecond;

        private GameObject _fire;
        private Player _player;

        #endregion


        #region Methods

        public void PutOutFire() {
            _fire.SetActive(false);
        }

        #endregion


        #region UnityMethods

        private void Start() {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
            _fire = GameObject.FindGameObjectWithTag("Fire");
        }

        private void OnTriggerStay(Collider collider) {
            if (collider.CompareTag("Player") && !_player.IsFireExtinguisherEquiped) {
                _player?.TakeDamage(_damagePerSecond * Time.deltaTime);
            }
        }

        #endregion
    }
}
