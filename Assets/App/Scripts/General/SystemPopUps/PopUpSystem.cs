using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using App.Scripts.General.SceneLoaderSpace;
using App.Scripts.General.Singleton;
using App.Scripts.General.SystemPopUps.PopUps;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpSystem : MonoSingleton<PopUpSystem>
    {
        [SerializeField] private PoolObjectInformation<PopUp>[] _popUpList;
        [SerializeField] private Canvas _canvasParent;
        private List<PopUp> _activePopUps = new List<PopUp>();

        private PopUpContainer _popUpContainer;

        public int ActivePopUpsCount => _activePopUps.Count;

        private void Start()
        {
            _canvasParent.worldCamera = FindObjectOfType<Camera>();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SetCamera;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SetCamera;
        }

        private void SetCamera(Scene arg0, LoadSceneMode arg1)
        {
            _canvasParent.worldCamera = FindObjectOfType<Camera>();
        }
        
        protected override void Awake()
        {
            base.Awake();
            _popUpContainer = new PopUpContainer(_popUpList, _canvasParent.transform);
        }

        public PopUp ShowPopUp<T>() where T : PopUp
        {
            var popUp = _popUpContainer.GetPopUpFromPoolByType(typeof(T));
            popUp!.OnPopUpClose += DeletePopUpFromActivePopUps;
            _activePopUps.Add(popUp);
            popUp.ShowPopUp();

            return popUp;
        }

        public void ShowInformationPopUp(string translateId)
        {
            var infoPopUp = (InformationPopUp)ShowPopUp<InformationPopUp>();
            infoPopUp.InitializeInformationPopUp(translateId);
        }
        
        private void DeletePopUpFromActivePopUps(PopUp popUp)
        {
            popUp.OnPopUpClose -= DeletePopUpFromActivePopUps;
            _popUpContainer.ReturnPopUpToPool(popUp);
            _activePopUps.Remove(popUp);
        }
    }
}