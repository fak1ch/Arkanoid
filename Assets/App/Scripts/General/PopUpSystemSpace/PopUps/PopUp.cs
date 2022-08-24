using System;
using System.Collections;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public abstract class PopUp : MonoBehaviour
    {
        public event Action<PopUp> OnPopUpClose;

        protected virtual void ShowAnimation()
        {
            gameObject.SetActive(true);
        }

        protected virtual void HideAnimation()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void ShowPopUp()
        {
            transform.SetAsFirstSibling();
            ShowAnimation();
        }

        protected void HidePopUp()
        {
            HideAnimation();
            ClosePopUpEvent();
        }

        private void ClosePopUpEvent()
        {
            OnPopUpClose?.Invoke(this);
        }
    }
}