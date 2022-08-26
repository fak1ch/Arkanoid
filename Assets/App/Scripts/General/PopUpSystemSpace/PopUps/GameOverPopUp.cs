using System;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class GameOverPopUp : PopUp
    {
        public event Action OnRestartButtonClicked;

        public void RestartButtonEvent()
        {
            HidePopUp();
            OnRestartButtonClicked?.Invoke();
        }
    }
}