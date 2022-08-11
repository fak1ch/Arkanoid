using System;
using UnityEngine;

namespace UISpace
{
    public class MainUI : MonoBehaviour
    {
        public event Action OnRestartGame;
        public event Action OnPauseTheGame;
        public event Action OnUnpauseTheGame;

        [SerializeField] private PausePanelUI _pauseMenu;
        [SerializeField] private GameOverPanelUI _gameOverMenu;
        [SerializeField] private YouWinPanelUI _youWinMenu;

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

        public void OpenPauseMenuEvent()
        {
            _pauseMenu.OpenPauseMenu();
        }

        public void OpenGameOverMenu()
        {
            _gameOverMenu.OpenMenu();
        }

        public void OpenYouWinMenu()
        {
            _youWinMenu.OpenMenu();
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
