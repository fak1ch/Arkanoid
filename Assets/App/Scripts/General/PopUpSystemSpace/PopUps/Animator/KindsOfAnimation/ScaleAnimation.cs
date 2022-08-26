using DG.Tweening;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace.PopUps.Animator.KindsOfAnimation
{
    public class ScaleAnimation : CustomAnimation
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _startValue;
        [SerializeField] private Vector3 _endValue;
        [SerializeField] private float _duration;

        private Sequence _scaleSequence;

        public override void Play()
        {
            base.Play();

            _transform.localScale = _startValue; 

            _transform.gameObject.SetActive(true);
            
            _scaleSequence = DOTween.Sequence();
            _scaleSequence.Insert(0, _transform.DOScale(_endValue, _duration));
            _scaleSequence.OnComplete(Stop);
        }
    }
}