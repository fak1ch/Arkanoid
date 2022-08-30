using System;
using System.Collections;
using App.Scripts.General.PopUpSystemSpace.PopUps.Animator;
using App.Scripts.General.SystemPopUps.PopUps.Animator;
using UnityEngine;

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
            
            _customAnimatorHide.StartAllAnimations();
            _customAnimatorHide.OnAnimationsEnd += PopUpClose;
        }

        private void PopUpOpen()
        {
            _customAnimatorShow.OnAnimationsEnd -= PopUpOpen;
            OnPopUpOpen?.Invoke(this);
        }
        
        private void PopUpClose()
        {
            _customAnimatorHide.OnAnimationsEnd -= PopUpClose;
            OnPopUpClose?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}