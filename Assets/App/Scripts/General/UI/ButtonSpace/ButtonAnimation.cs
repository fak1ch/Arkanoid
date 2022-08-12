using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace ButtonSpace
{
    public class ButtonAnimation : Button
    {
        public ButtonClickedEvent onClickMy;

        public RectTransform rectTransform;
        public Image buttonImage;
        public ButtonScriptableObject settings;

        private Color _normalColor;
        private Vector3 _startScale;
        private bool _isPointerExit = true;

        protected override void Start()
        {
            _startScale = rectTransform.localScale;
            _normalColor = buttonImage.color;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _isPointerExit = false;
            ButtonPressed();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (_isPointerExit == false)
                ButtonUnPressed();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            _isPointerExit = true;
            ButtonUnPressed();
        }

        private void ButtonPressed()
        {
            rectTransform.DOScale(rectTransform.localScale * settings.PressedScaleProcent, settings.ScaleDuration);
            buttonImage.DOColor(settings.PressedColor, settings.ScaleDuration);
        }

        private void ButtonUnPressed()
        {
            rectTransform.DOScale(_startScale, settings.ScaleDuration);
            buttonImage.DOColor(_normalColor, settings.ScaleDuration);
            ButtonUnPressedComplete();
        }

        private void ButtonUnPressedComplete()
        {
            if (_isPointerExit == false)
            {
                onClickMy?.Invoke();
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }
    }
}
