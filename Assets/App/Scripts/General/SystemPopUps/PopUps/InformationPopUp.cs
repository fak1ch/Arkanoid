using App.Scripts.General.LocalizationSystemSpace;
using App.Scripts.General.PopUpSystemSpace;
using UnityEngine;

namespace App.Scripts.General.SystemPopUps.PopUps
{
    public class InformationPopUp : PopUp
    {
        [SerializeField] private TranslatableText _translatableText;

        public void InitializeInformationPopUp(string translatableId)
        {
            _translatableText.SetId(translatableId);
        }

        public void ContinueButtonEvent()
        {
            HidePopUp();
        }
    }
}