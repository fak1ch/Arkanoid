using System;
using System.Collections;
using App.Scripts.General.Energy;
using App.Scripts.General.SceneLoaderSpace;
using GameEventsControllerSpace;
using UISpace;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class PauseGamePopUp : PopUp
    {
        [SerializeField] private float _delayUnitlRestart;
        
        public override void ShowPopUp()
        {
            GameEventsController.Instance.PauseTheGame();
            base.ShowPopUp();
        }

        public void RestartButtonEvent()
        {
            if (EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice))
            {
                StartCoroutine(RestartGame());
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

        private IEnumerator RestartGame()
        {
            EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
            yield return new WaitForSeconds(_delayUnitlRestart);
            HidePopUp();
            GameEventsController.Instance.RestartGame();
        }
    }
}