using UnityEngine;
using UnityEngine.AI;


namespace EscapeRoom {

    public class EnemyPatroling : MonoBehaviour {

        #region Fields

        [SerializeField] private NavMeshAgent _navAgent;
        [SerializeField] private Transform[] _waypoints;

        private int _currentWaypointIndex;

        #endregion


        #region Methods

        private void Move2NextWaypoint() {
            if (_navAgent.remainingDistance < _navAgent.stoppingDistance) {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                _navAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
            }
        }

        #endregion

        #region UnityMethods

        void Start() {
            _currentWaypointIndex = 0;
            _navAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
        }

        void Update() {
            Move2NextWaypoint();
        }

        #endregion
    }
}
