using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace ButtonSpace
{
    public class ButtonAnimation : Button
    {
        public ButtonClickedEvent onClickEvent;

        public RectTransform rectTransform;
        public Image buttonImage;
        public ButtonScriptableObject settings;

        private Color _normalColor;
        private Vector3 _startScale;
        private bool _isPointerExit = true;
        private bool _isPointerDown = false;

        private Sequence _pressedSequence;
        private Sequence _unPressedSequence;

        protected override void Start()
        {
            _startScale = rectTransform.localScale;
            _normalColor = buttonImage.color;

            _pressedSequence = CreateSequence(_startScale * settings.PressedScaleProcent, settings.ScaleDuration, 
                settings.PressedColor, settings.ScaleDuration);

            _unPressedSequence = CreateSequence(_startScale, settings.ScaleDuration, 
                _normalColor, settings.ScaleDuration);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
            _isPointerExit = false;
            ButtonPressed();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;
            
            if (_isPointerExit == false)
                ButtonUnPressed();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (_isPointerDown == true)
            {
                _isPointerExit = true;
                ButtonUnPressed();
            }
            
            _isPointerDown = false;
        }

        private void ButtonPressed()
        {
            if (_pressedSequence.IsComplete())
                _pressedSequence.Restart();
            
            _pressedSequence.Play();
        }

        private void ButtonUnPressed()
        {
            _pressedSequence.Complete();
            
            if (_unPressedSequence.IsComplete())
                _unPressedSequence.Restart();
            
            _unPressedSequence.Play();

            _unPressedSequence.OnComplete(ButtonUnPressedComplete);
        }

        private void ButtonUnPressedComplete()
        {
            if (_isPointerExit == false)
            {
                _pressedSequence.Pause();
                _unPressedSequence.Pause();

                onClickEvent?.Invoke();
            }
        }

        private Sequence CreateSequence(Vector3 endScaleValue, float duration, Color endColor, float colorDuration)
        {
            var tween1 = rectTransform.DOScale(endScaleValue, duration);
            var tween2 = buttonImage.DOColor(endColor, colorDuration);
            
            Sequence sequence = DOTween.Sequence();
            sequence.Pause();
            sequence.Append(tween1);
            sequence.Insert(0, tween2);
            sequence.SetAutoKill(false);

            return sequence;
        }
    }
}
