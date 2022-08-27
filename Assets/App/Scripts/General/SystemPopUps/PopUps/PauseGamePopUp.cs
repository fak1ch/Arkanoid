using System;
using App.Scripts.General.SceneLoaderSpace;
using GameEventsControllerSpace;
using UISpace;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class PauseGamePopUp : PopUp
    {
        public override void ShowPopUp()
        {
            GameEventsController.Instance.PauseTheGame();
            base.ShowPopUp();
        }

        public void RestartButtonEvent()
        {
            HidePopUp();
            GameEventsController.Instance.RestartGame();
        }

        public void BackButtonEvent()
        {
            SceneLoader.Instance.LoadSceneById(1);
            HidePopUp();
        }

        public void ContinueButtonEvent()
        {
            HidePopUp();
            GameEventsController.Instance.UnpauseTheGame();
        }
    }
}