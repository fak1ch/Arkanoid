using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISpace
{
    public class PausePanelUI : MonoBehaviour
    {
        public event Action OnRestartButtonClicked;
        public event Action OnPauseTheGame;
        public event Action OnUnpauseTheGame;

        public void OpenPauseMenu()
        {
            gameObject.SetActive(true);
            OnPauseTheGame?.Invoke();
        }

        public void RestartButtonEvent()
        {
            OnRestartButtonClicked?.Invoke();
            ContinueButtonEvent();
        }

        public void BackButtonEvent()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ContinueButtonEvent()
        {
            gameObject.SetActive(false);
            OnUnpauseTheGame?.Invoke();
        }
    }
}
