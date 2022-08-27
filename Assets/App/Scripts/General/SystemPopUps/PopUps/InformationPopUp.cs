using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.SceneLoaderSpace;
using GameEventsControllerSpace;
using TMPro;
using UnityEngine;

namespace App.Scripts.General.SystemPopUps.PopUps
{
    public class InformationPopUp : PopUp
    {
        [SerializeField] private TextMeshProUGUI _infoText;

        public void InitializeInformationPopUp(string message)
        {
            _infoText.text = message;
        }

        public void ContinueButtonEvent()
        {
            HidePopUp();
        }
    }
}