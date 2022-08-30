using System.Collections;
using App.Scripts.General.Energy;
using GameEventsControllerSpace;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class GameOverPopUp : PopUp
    {
        [SerializeField] private float _delayUnitlRestart;
        [SerializeField] private float _delayAfterBuyLive;
        [SerializeField] private int _addHealthValue;
        [SerializeField] private int _minusEnergyValue;

        public void RestartButtonEvent()
        {
            if (EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice))
            {
                StartCoroutine(RestartGame());
                OnPopUpClose += PopUpClose;
            }
        }

        private void PopUpClose(PopUp popUp)
        {
            OnPopUpClose -= PopUpClose;
        }
        
        public void BuyLiveButtonEvent()
        {
            if (EnergySystem.Instance.IsEnoughEnergy(_minusEnergyValue))
            {
                StartCoroutine(BuyLiveRoutine());
            }
        }
        
        private IEnumerator RestartGame()
        {
            SetButtonsInteractable(false);
            EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
            yield return new WaitForSeconds(_delayUnitlRestart);
            HidePopUp();
            GameEventsController.Instance.RestartGame();
        }

        private IEnumerator BuyLiveRoutine()
        {
            GameEventsController.Instance.GetPlayerHealth().AddHealth(_addHealthValue);
            EnergySystem.Instance.MinusEnergy(_minusEnergyValue);
            yield return new WaitForSeconds(_delayAfterBuyLive);
            HidePopUp();
            GameEventsController.Instance.UnpauseTheGame();
        }
    }
}