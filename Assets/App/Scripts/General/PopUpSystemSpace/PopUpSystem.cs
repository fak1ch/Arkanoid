using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpSystem : MonoBehaviour
    {
        [SerializeField] private List<PopUp> _popUpList = new List<PopUp>();
        private List<PopUp> _activePopUps = new List<PopUp>();

        public void ShowPopUp<T>() where T : PopUp
        {
            var popUp = _popUpList.FirstOrDefault(popUp => popUp.GetType() == typeof(T));
            popUp!.OnPopUpClose += DeletePopUpFromActivePopUps;
            _activePopUps.Add(popUp);
            popUp.ShowPopUp();
        }

        private void DeletePopUpFromActivePopUps(PopUp popUp)
        {
            popUp.OnPopUpClose -= DeletePopUpFromActivePopUps;
            _activePopUps.Remove(popUp);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ShowPopUp<PauseGamePopUp>();
            }
        }
    }
}