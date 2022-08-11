using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISpace
{
    public class GameOverPanelUI : MonoBehaviour
    {
        public event Action OnRestartButtonClicked;

        public void OpenMenu()
        {
            gameObject.SetActive(true);
        }

        public void RestartButtonEvent()
        {
            OnRestartButtonClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
