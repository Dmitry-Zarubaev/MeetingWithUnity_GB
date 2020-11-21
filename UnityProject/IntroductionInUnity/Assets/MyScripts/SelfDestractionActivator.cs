using UnityEngine;


namespace EscapeRoom {

    public class SelfDestractionActivator : Door {

        #region Fields

        [SerializeField] private Animator _crazyDoor;
        [SerializeField] private GameData _gameData;

        private UserInterface _uiController;
        private Player _player;

        private float _second = 1.0f;
        private int _seflDestructionTime;
        private bool _isActive = false;

        #endregion


        #region Methods

        public void DeactivateSelfDestruction() {
            _uiController.SetActiveCountdown(false);
            _isActive = false;

        }

        private void DoCountdown() {
            _second -= Time.deltaTime;

            if (_second <= 0.0f) {
                _seflDestructionTime--;
                _second = 1.0f;

                _uiController.SetCountdownCounter(_seflDestructionTime);

                if (_seflDestructionTime <= 0) {
                    DeactivateSelfDestruction();
                    _player.Die();
                }
            }
        }

        private void ActivateSelfDestruction() {
            _crazyDoor.SetBool("isAnimated", true);
            _uiController.SetActiveCountdown(true, _seflDestructionTime);
            _isActive = true;
        }

        #endregion


        #region UnityMethods

        private void Start() {
            _seflDestructionTime = _gameData.SelfDestructionTime;
            _player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
            _uiController = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UserInterface>();
        }

        private void FixedUpdate() {
            if (_isActive) {
                DoCountdown();
            }
        }

        private void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.CompareTag("Player")) {
                Debug.Log(collider.name);

                CloseDoor();
                ActivateSelfDestruction();
            }
        }

        #endregion
    }
}
