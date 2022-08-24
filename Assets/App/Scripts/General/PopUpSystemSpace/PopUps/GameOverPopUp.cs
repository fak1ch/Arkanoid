using System;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class GameOverPopUp : FadeScalePopUp
    {
        public event Action OnRestartButtonClicked;

        public void RestartButtonEvent()
        {
            HidePopUp();
            OnRestartButtonClicked?.Invoke();
        }
    }
}