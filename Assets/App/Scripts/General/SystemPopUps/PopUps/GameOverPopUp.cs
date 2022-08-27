using System;
using App.Scripts.General.Energy;
using GameEventsControllerSpace;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class GameOverPopUp : PopUp
    {
        public void RestartButtonEvent()
        {
            if (EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice))
            {
                EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
                HidePopUp();
                GameEventsController.Instance.RestartGame();
            }
        }
    }
}