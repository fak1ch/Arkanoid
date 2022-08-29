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
        [SerializeField] private float _raysDuration;
        [SerializeField] private float _raysAngle;
        
        [Space(10)]
        [SerializeField] private Transform _galaxyIcon;
        [SerializeField] private Image _rays;
        [SerializeField] private Transform _energy;
        [SerializeField] private Transform _youWinText;
        [SerializeField] private Transform _buttonContinue;
        [SerializeField] private GameObject _confettiEffect;

        private Sequence _mainSequence;

        public override void Play()
        {
            base.Play();
            
            _buttonContinue.DOScale(0, 0);
            _youWinText.DOScale(0, 0);
            _galaxyIcon.DOScale(0, 0);
            _energy.DOScale(0, 0);

            _rays.transform.DORotate(new Vector3(0, 0, _raysAngle), _raysDuration)
                .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            
            _mainSequence = DOTween.Sequence();
            _mainSequence.Append(_galaxyIcon.DOScale(1, _duration));
            _mainSequence.Append(_youWinText.DOScale(1, _duration));
            _mainSequence.Append(_energy.DOScale(1, _duration).SetEase(Ease.OutCirc));
            _mainSequence.Append(_buttonContinue.DOScale(1, _duration)
                .OnComplete(() => Instantiate(_confettiEffect)));

            _mainSequence.OnComplete(Stop);
        }
    }
}