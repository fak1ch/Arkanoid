using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class FadeScalePopUp : PopUp
    {
        [SerializeField] private Image _raycastBg;
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private float _animationSpeed = 0.4f;
        
        protected override void ShowAnimation()
        {
            gameObject.SetActive(true);
            _raycastBg.DOFade(0, 0);
            _raycastBg.DOFade(0.5f, _animationSpeed);
            _mainPanel.transform.DOScale(Vector3.zero, 0);
            _mainPanel.transform.DOScale(Vector3.one, _animationSpeed);
        }

        protected override void HideAnimation()
        {
            _raycastBg.DOFade(0f, _animationSpeed);
            _mainPanel.transform.DOScale(Vector3.zero, _animationSpeed).OnComplete(HideAnimationComplete);
        }

        private void HideAnimationComplete()
        {
            gameObject.SetActive(false);
        }
    }
}