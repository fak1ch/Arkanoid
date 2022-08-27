using System;
using App.Scripts.General.Energy;
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
            if (EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice))
            {
                EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
                HidePopUp();
                GameEventsController.Instance.RestartGame();
            }
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