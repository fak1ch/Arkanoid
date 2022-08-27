using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace.PopUps.Animator
{
    public class CustomAnimation : MonoBehaviour
    {
        public event Action OnStart;
        public event Action<CustomAnimation> OnEnd;
        
        public bool IsPlay { get; private set; }

        public virtual void Play()
        {
            OnStart?.Invoke();
            IsPlay = true;
        }

        public void Stop()
        {
            OnEnd?.Invoke(this);
            IsPlay = false;
        }
    }
}