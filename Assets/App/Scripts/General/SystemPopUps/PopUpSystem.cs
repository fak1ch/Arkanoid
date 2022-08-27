using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using App.Scripts.General.Singleton;
using App.Scripts.General.SystemPopUps.PopUps;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpSystem : MonoSingleton<PopUpSystem>
    {
        [SerializeField] private PoolObjectInformation<PopUp>[] _popUpList;
        [SerializeField] private Transform _canvasParent;
        private List<PopUp> _activePopUps = new List<PopUp>();

        private PopUpContainer _popUpContainer;

        protected override void Awake()
        {
            base.Awake();
            _popUpContainer = new PopUpContainer(_popUpList, _canvasParent);
        }

        public PopUp ShowPopUp<T>() where T : PopUp
        {
            var popUp = _popUpContainer.GetPopUpFromPoolByType(typeof(T));
            popUp!.OnPopUpClose += DeletePopUpFromActivePopUps;
            _activePopUps.Add(popUp);
            popUp.ShowPopUp();

            return popUp;
        }

        public void ShowInformationPopUp(string message)
        {
            var infoPopUp = (InformationPopUp)ShowPopUp<InformationPopUp>();
            infoPopUp.InitializeInformationPopUp(message);
        }
        
        private void DeletePopUpFromActivePopUps(PopUp popUp)
        {
            popUp.OnPopUpClose -= DeletePopUpFromActivePopUps;
            _popUpContainer.ReturnPopUpToPool(popUp);
            _activePopUps.Remove(popUp);
        }
    }
}