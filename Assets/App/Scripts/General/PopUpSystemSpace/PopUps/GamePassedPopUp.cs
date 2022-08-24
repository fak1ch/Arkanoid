using System;
using System.Collections;
using System.Linq;
using App.Scripts.General.SceneLoaderSpace;
using App.Scripts.Scenes.SelectingPack;
using DG.Tweening;
using LevelGeneration;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class GamePassedPopUp : FadeScalePopUp
    {
        [SerializeField] private PackScriptableObject _packScriptableObject;
        [SerializeField] private float _animDuration;
        [SerializeField] private float _raysAngle;
        [SerializeField] private Transform _galaxyIcon;
        [SerializeField] private Image _rays;
        [SerializeField] private Transform _energy;
        [SerializeField] private Transform _buttonContinue;

        private Sequence _mainSequence;
        private Sequence _raysSequence;

        protected override void ShowAnimation()
        {
            _buttonContinue.DOScale(0, 0);
            _galaxyIcon.DOScale(0, 0);
            _energy.DOScale(0, 0);
            _rays.DOFade(0, 0);

            _raysSequence = DOTween.Sequence();
            _raysSequence.Pause();
            _raysSequence.Append(_rays.DOFade(1, _animDuration));
            _raysSequence.Insert(0, _rays.transform.DORotate(
                new Vector3(0, 0, _raysAngle), _animDuration));
            
            base.ShowAnimation();
            
            _mainSequence = DOTween.Sequence();
            _mainSequence.Append(_galaxyIcon.DOScale(1, _animDuration));
            _mainSequence.Append(_energy.DOScale(1, _animDuration));
            _mainSequence.Append(_raysSequence.Play());
            _mainSequence.Append(_buttonContinue.DOScale(1, _animDuration));
        }

        public void ContinueButtonEvent()
        {
            var currentPack = new PackRepository(GetPackInfoById(StaticLevelPath.packId));

            if (StaticLevelPath.packId != -1)
                currentPack.LevelComplete();

            if (currentPack.CurrentLevelIndex == currentPack.LevelCount)
            {
                SceneLoader.Instance.LoadSceneById(1);
            }
            else
            {
                StaticLevelPath.levelPath = currentPack.GetLevelPath();
                SceneLoader.Instance.LoadSceneById(2);
            }
            
            HidePopUp();
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}