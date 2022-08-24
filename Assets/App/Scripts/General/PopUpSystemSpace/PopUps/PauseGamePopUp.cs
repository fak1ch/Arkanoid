using System;
using App.Scripts.General.SceneLoaderSpace;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class PauseGamePopUp : FadeScalePopUp
    {
        public event Action OnRestartButtonClicked;
        public event Action OnPauseTheGame;
        public event Action OnUnpauseTheGame;

        public override void ShowPopUp()
        {
            OnPauseTheGame?.Invoke();
            base.ShowPopUp();
        }

        public void RestartButtonEvent()
        {
            HidePopUp();
            OnRestartButtonClicked?.Invoke();
        }

        public void BackButtonEvent()
        {
            SceneLoader.Instance.LoadSceneById(1);
            HidePopUp();
        }

        public void ContinueButtonEvent()
        {
            HidePopUp();
            OnUnpauseTheGame?.Invoke();
        }
    }
}