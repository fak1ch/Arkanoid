using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Live : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _fullLiveColor;
        [SerializeField] private Color _emptyLiveColor;

        public bool IsFullLive { get; private set; }

        private void Start()
        {
            SetLiveAsFull();
        }

        public void SetLiveAsEmpty()
        {
            _image.color = _emptyLiveColor;
            IsFullLive = false;
        }
        
        public void SetLiveAsFull()
        {
            _image.color = _fullLiveColor;
            IsFullLive = true;
        }
    }
}