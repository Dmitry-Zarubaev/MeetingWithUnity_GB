using UnityEngine;
using UnityEngine.AI;


namespace EscapeRoom {

    public class Enemy : MonoBehaviour, IDamageable {

        #region PrivateData

        private enum EnemyState {
            None = 0,
            Alive = 1,
            Stunned = 2
        }

        #endregion


        #region Fields

        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private GameData _gameData;

        private NavMeshAgent _navAgent;

        private EnemyState _enemyState = EnemyState.Alive;

        private float _stunTime;
        private float _health;

        private int _currentWaypointIndex;

        #endregion


        #region Methods

        private void Move2NextWaypoint() {
            if (_navAgent.remainingDistance < _navAgent.stoppingDistance) {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                _navAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
            }
        }

        private void DoUnstun() {
            _stunTime -= Time.deltaTime;

            if (_stunTime <= 0.0f) {
                _stunTime = _gameData.ZombieStunTime;
                _navAgent.isStopped = false;
                _enemyState = EnemyState.Alive;
            }
        }

        private void Die() {
            gameObject.SetActive(false);
            Destroy(this);
        }

        public void HealDamage(float heal) {

        }

        public void TakeDamage(float damage, Damagedealers damageType) {
            _health -= damage;

            if (_health <= 0.0f) {
                switch (damageType) {
                    case Damagedealers.Scrap:
                        _enemyState = EnemyState.Stunned;
                        _navAgent.isStopped = true;
                        print("#debug. Enemy has been stunned");
                        break;
                    case Damagedealers.Landmine:
                        Die();
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion


        #region UnityMethods

        private void Start() {
            _currentWaypointIndex = 0;

            _health = _gameData.ZombieMaxHealth;
            _stunTime = _gameData.ZombieStunTime;

            _navAgent = GetComponentInChildren<NavMeshAgent>();
            _navAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
        }

        private void Update() {
            switch (_enemyState) {
                case EnemyState.Alive:
                    Move2NextWaypoint();
                    break;
                case EnemyState.Stunned:
                    DoUnstun();
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.CompareTag("Player") && _enemyState != EnemyState.Stunned) {
                Player player = collider.GetComponent<Player>();
                player.TakeDamage(_gameData.ZombieBiteDamage, Damagedealers.ZombieBite);
            }

            if (collider.CompareTag("Enemy")) {
                _currentWaypointIndex = 0;
            }
        }

        #endregion
    }
}
