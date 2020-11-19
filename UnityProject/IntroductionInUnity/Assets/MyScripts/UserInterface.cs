﻿using UnityEngine;
using UnityEngine.UI;


namespace EscapeRoom {

    public class UserInterface : MonoBehaviour {

        #region Fields

        [SerializeField] private Image _healthBar;
        [SerializeField] private Text _scrapCounter;
        [SerializeField] private Text _inventory;

        [SerializeField] private GameObject _countdown;
        [SerializeField] private GameObject _gameOver;

        private Text _gameOverLabel;
        private Text _countdownLabel;

        #endregion


        #region UnityMethods

        private void Start() {
            _gameOverLabel = _gameOver.GetComponentInChildren<Text>();
            _countdownLabel = _countdown.GetComponentInChildren<Text>();
        }

        #endregion


        #region Methods

        public void SetHealthBar(float health) {
            _healthBar.fillAmount = health;
        }

        public void SetScrapCounter(int scrap) {
            _scrapCounter.text = scrap.ToString();
        }

        public void SetInventory(string itemName) {
            _inventory.text = itemName;
        }

        public void SetActiveCountdown(bool active) {
            _countdown.SetActive(active);
        }

        public void SetCountdownCounter(int seconds) {
            int _minutes = seconds / 60;
            int _seconds = seconds % 60;

            _countdownLabel.text = _minutes.ToString() + ":" + _seconds.ToString();
        }

        public void SetGameOver(bool isWin) {
            if (isWin) {
                Color green = new Color(0.0f, 1.0f, 0.0f);

                _gameOverLabel.text = "ESCAPED";
                _gameOverLabel.color = green;
            } else {
                Color red = new Color(1.0f, 0.0f, 0.0f);

                _gameOverLabel.text = "RUINED";
                _gameOverLabel.color = red;
            }

            _gameOver.SetActive(true);
        }

        #endregion
    }
}
