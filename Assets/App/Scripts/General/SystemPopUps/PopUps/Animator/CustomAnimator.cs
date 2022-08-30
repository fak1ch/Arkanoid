using System;
using System.Collections.Generic;
using App.Scripts.General.PopUpSystemSpace.PopUps.Animator;
using UnityEngine;

namespace App.Scripts.General.SystemPopUps.PopUps.Animator
{
    public class CustomAnimator : MonoBehaviour
    {
        public event Action OnAnimationsStart;
        public event Action OnAnimationsEnd;
        
        [SerializeField] private List<CustomAnimation> _anims = new List<CustomAnimation>();

        private int _stoppedAnimationsCount;
        
        public void StartAllAnimations()
        {
            OnAnimationsStart?.Invoke();
            _stoppedAnimationsCount = _anims.Count;

            foreach (var anim in _anims)
            {
                anim.Play();
                anim.OnEnd += OneAnimationStop;
            }
        }

        public void StopAllAnimations()
        {
            foreach (var anim in _anims)
            {
                if (anim.IsPlay)
                {
                    anim.Stop();
                }
            }
        }
        
        private void OneAnimationStop(CustomAnimation anim)
        {
            anim.OnEnd -= OneAnimationStop;
            _stoppedAnimationsCount--;

            if (_stoppedAnimationsCount == 0)
            {
                OnAnimationsEnd?.Invoke();
            }
        }
    }
}