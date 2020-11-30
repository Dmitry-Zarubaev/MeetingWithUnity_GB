using UnityEngine;


namespace EscapeRoom {

    public class Scrap : MonoBehaviour {

        #region Fields

        [SerializeField] private GameData _gameData;

        private Rigidbody _rigidbody;

        #endregion


        #region UnityMethods

        private void Start() {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            Vector3 impulse = transform.up * _gameData.ScrapInitialSpeed;
            _rigidbody.AddForce(impulse, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.CompareTag("Enemy")) {
                Enemy enemy = collider.GetComponent<Enemy>();
                print("#debug. Scrap has hited Enemy");

                enemy.TakeDamage(_gameData.ScrapDamage, Damagedealers.Scrap);
            }
        }

        #endregion
    }
}
