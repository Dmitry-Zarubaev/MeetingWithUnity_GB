using System;
using UnityEngine;


namespace EscapeRoom {

    public class Landmine : MonoBehaviour {

        #region Fields

        [SerializeField] private GameData _gameData;
        [SerializeField] private Material _engagedMarker;

        private float _engagingTime = 0.0f;
        private bool _isEngaged = false;

        #endregion

        #region Methods

        private void Engage() {
            if (!_isEngaged) {
                _engagingTime -= Time.deltaTime;

                if (_engagingTime <= 0.0f) {
                    MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
                    MeshRenderer detonator = Array.Find<MeshRenderer>(meshes, text => text.name == "Detonator");

                    detonator.material = _engagedMarker;
                    _isEngaged = true;
                }
            }
        }

        #endregion

        #region UnityMethods

        private void Start() {
            _engagingTime = _gameData.LandmineEngagingTime;
        }

        private void Explode() {
            print("#debug. Landmine has exploded");
            DestroyImmediate(gameObject);
        }

        private void LateUpdate() {
            Engage();
        }

        private void OnTriggerEnter(Collider collider) {
            if (_isEngaged) {

                if (collider.CompareTag("Player")) {
                    Player player;
                    collider.TryGetComponent(out player);

                    if (player != null) {
                        player.TakeDamage(_gameData.LandmineDamage, Damagedealers.Landmine);
                        Invoke(nameof(Explode), 0);
                    }
                }

                if (collider.CompareTag("Enemy")) {
                    Enemy enemy;
                    collider.TryGetComponent(out enemy);

                    if (enemy != null) {
                        enemy.TakeDamage(_gameData.LandmineDamage, Damagedealers.Landmine);
                        Invoke(nameof(Explode), 0);
                    }
                }
            }
        }

        #endregion
    }
}
