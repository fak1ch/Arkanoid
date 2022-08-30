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

        private bool _buttonInteractableOpen = true;
        
        public override void ShowPopUp()
        {
            GameEventsController.Instance.PauseTheGame();
            base.ShowPopUp();
            
            OnPopUpClose += PopUpClose;
        }

        private void PopUpClose(PopUp popUp)
        {
            OnPopUpClose -= PopUpClose;
            _buttonInteractableOpen = true;
        }
        
        public void RestartButtonEvent()
        {
            if (!_buttonInteractableOpen) return;
            
            if (EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice))
            {
                StartCoroutine(RestartGame());
                _buttonInteractableOpen = false;
            }
        }

        public void BackButtonEvent()
        {
            if (!_buttonInteractableOpen) return;
            _buttonInteractableOpen = false;
            
            SceneLoader.Instance.LoadScene(SceneEnum.SelectingPack);
            HidePopUp();
        }

        public void ContinueButtonEvent()
        {
            if (!_buttonInteractableOpen) return;
            _buttonInteractableOpen = false;
            
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