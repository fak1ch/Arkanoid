using System.Collections;
using App.Scripts.General.Energy;
using App.Scripts.General.LoadScene;
using GameEventsControllerSpace;
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
            
            OnPopUpClose += PopUpClose;
        }

        private void PopUpClose(PopUp popUp)
        {
            OnPopUpClose -= PopUpClose;
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
            SceneLoader.Instance.LoadScene(SceneEnum.SelectingPack);
            HidePopUp();
        }

        public void ContinueButtonEvent()
        {
            HidePopUp();
            GameEventsController.Instance.UnpauseTheGame();
        }

        private IEnumerator RestartGame()
        {
            SetButtonsInteractable(false);
            EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
            yield return new WaitForSeconds(_delayUnitlRestart);
            HidePopUp();
            GameEventsController.Instance.RestartGame();
        }
    }
}