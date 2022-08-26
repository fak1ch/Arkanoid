using App.Scripts.General.LocalizationSystemSpace;
using App.Scripts.Scenes.SelectingPack;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.General.PopUpSystemSpace.PopUps.Animator.KindsOfAnimation
{
    public class GamePassedAnimation : CustomAnimation
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _raysAngle;
        
        [Space(10)]
        [SerializeField] private Transform _galaxyIcon;
        [SerializeField] private Image _rays;
        [SerializeField] private Transform _energy;
        [SerializeField] private Transform _youWinText;
        [SerializeField] private Transform _buttonContinue;

        private Sequence _mainSequence;
        private Sequence _raysSequence;

        public override void Play()
        {
            base.Play();
            
            _buttonContinue.DOScale(0, 0);
            _youWinText.DOScale(0, 0);
            _galaxyIcon.DOScale(0, 0);
            _energy.DOScale(0, 0);
            _rays.DOFade(0, 0);
            
            _raysSequence = DOTween.Sequence();
            _raysSequence.Pause();
            _raysSequence.Append(_rays.DOFade(1, _duration));
            _raysSequence.Insert(0, _rays.transform.DORotate(
                new Vector3(0, 0, _raysAngle), _duration)).SetLoops(-1);

            _mainSequence = DOTween.Sequence();
            _mainSequence.Append(_galaxyIcon.DOScale(1, _duration));
            _mainSequence.Append(_youWinText.DOScale(1, _duration));
            _mainSequence.Append(_energy.DOScale(1, _duration));
            _mainSequence.Append(_raysSequence.Play());
            _mainSequence.Append(_buttonContinue.DOScale(1, _duration));
        }
    }
}