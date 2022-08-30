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

        private bool _buttonRestartOpen = true;
        
        public void RestartButtonEvent()
        {
            if (!_buttonRestartOpen) return;
            
            if (EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice))
            {
                StartCoroutine(RestartGame());
                _buttonRestartOpen = false;
                OnPopUpClose += PopUpClose;
            }
        }

        private void PopUpClose(PopUp popUp)
        {
            OnPopUpClose -= PopUpClose;
            _buttonRestartOpen = true;
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