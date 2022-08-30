using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.General.PopUpSystemSpace.PopUps.Animator;
using App.Scripts.General.SystemPopUps.PopUps.Animator;
using App.Scripts.General.UI.ButtonSpace;
using UnityEngine;
using UnityEngine.UIElements;

namespace App.Scripts.General.PopUpSystemSpace
{
    public abstract class PopUp : MonoBehaviour
    {
        public event Action<PopUp> OnPopUpStartHideAnimation;
        public event Action<PopUp> OnPopUpStartShowAnimation;
        public event Action<PopUp> OnPopUpOpen;
        public event Action<PopUp> OnPopUpClose;

        [SerializeField] private CustomAnimator _customAnimatorShow;
        [SerializeField] private CustomAnimator _customAnimatorHide;
        [SerializeField] private List<ButtonAnimation> _buttons;

        public virtual void ShowPopUp()
        {
            OnPopUpStartShowAnimation?.Invoke(this);

            transform.SetAsLastSibling();
            _customAnimatorShow.StartAllAnimations();
            _customAnimatorShow.OnAnimationsEnd += PopUpOpen;

            gameObject.SetActive(true);
        }

        protected void HidePopUp()
        {
            OnPopUpStartHideAnimation?.Invoke(this);

            SetButtonsInteractable(false);
            _customAnimatorHide.StartAllAnimations();
            _customAnimatorHide.OnAnimationsEnd += PopUpClose;
        }

        private void PopUpOpen()
        {
            _customAnimatorShow.OnAnimationsEnd -= PopUpOpen;
            OnPopUpOpen?.Invoke(this);
            SetButtonsInteractable(true);
        }
        
        private void PopUpClose()
        {
            _customAnimatorHide.OnAnimationsEnd -= PopUpClose;
            OnPopUpClose?.Invoke(this);
            gameObject.SetActive(false);
        }

        protected void SetButtonsInteractable(bool value)
        {
            foreach (var button in _buttons)
            {
                button.interactable = value;
            }
        }
    }
}