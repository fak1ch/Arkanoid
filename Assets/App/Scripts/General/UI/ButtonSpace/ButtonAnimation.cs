using ButtonSpace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.General.UI.ButtonSpace
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Transform _button;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private ButtonScriptableObject _settings;

        [SerializeField] private UnityEvent _calledMethod;
        
        private Vector3 _startScale;
        private Color _startColor;
        private Tween _scaleTween;
        private Tween _colorTween;
        private bool _onPointerClickOpen = false;
        private bool _pointerExit = false;

        private void Awake()
        {
            _startScale = _button.transform.localScale;
            _startColor = _buttonImage.color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _scaleTween = _button.transform.DOScale(
                _startScale * _settings.pressedScalePercent, + _settings.scaleDuration);
            _colorTween = _buttonImage.DOColor(_settings.pressedColor, _settings.changeColorDuration);

            _onPointerClickOpen = false;
            _pointerExit = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _scaleTween = _button.transform.DOScale(
                _startScale, _settings.scaleDuration);
            _colorTween = _buttonImage.DOColor(_startColor, _settings.changeColorDuration);

            if (_pointerExit) return;
            
            _onPointerClickOpen = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _scaleTween = _button.transform.DOScale(
                _startScale, _settings.scaleDuration);
            _colorTween = _buttonImage.DOColor(_startColor, _settings.changeColorDuration);

            _onPointerClickOpen = false;
            _pointerExit = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_onPointerClickOpen)
            {
                _scaleTween.OnComplete(() => _calledMethod.Invoke());
            }
        }
    }
}