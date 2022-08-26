using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.General.PopUpSystemSpace.PopUps.Animator.KindsOfAnimation
{
    public class ImageFadeAnimation : CustomAnimation
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _startValue;
        [SerializeField] private float _endValue;
        [SerializeField] private float _duration;

        private Sequence _imageFadeSequence;

        public override void Play()
        {
            base.Play();
            
            var color = _image.color;
            color.a = _startValue;
            _image.color = color;

            _image.gameObject.SetActive(true);
            
            _imageFadeSequence = DOTween.Sequence();
            _imageFadeSequence.Insert(0, _image.DOFade(_endValue, _duration));
            _imageFadeSequence.OnComplete(Stop);
        }
    }
}