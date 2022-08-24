using System;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using UnityEngine;

namespace UISpace
{
    public class PopUpTransmitter : MonoBehaviour
    {
        public event Action OnRestartGame;
        public event Action OnPauseTheGame;
        public event Action OnUnpauseTheGame;

        [SerializeField] private PauseGamePopUp _pauseMenu;
        [SerializeField] private GameOverPopUp _gameOverMenu;

        private void OnEnable()
        {
            _pauseMenu.OnRestartButtonClicked += RestartGame;
            _pauseMenu.OnPauseTheGame += PauseTheGame;
            _pauseMenu.OnUnpauseTheGame += UnpauseTheGame;
            _gameOverMenu.OnRestartButtonClicked += RestartGame;
        }

        private void OnDisable()
        {
            _pauseMenu.OnRestartButtonClicked -= RestartGame;
            _pauseMenu.OnPauseTheGame -= PauseTheGame;
            _pauseMenu.OnUnpauseTheGame -= UnpauseTheGame;
            _gameOverMenu.OnRestartButtonClicked -= RestartGame;
        }

        private void RestartGame()
        {
            OnRestartGame?.Invoke();
        }

        private void PauseTheGame()
        {
            OnPauseTheGame?.Invoke();
        }

        private void UnpauseTheGame()
        {
            OnUnpauseTheGame?.Invoke();
        }
    }
}
