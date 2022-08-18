using System;
using BallSpace;
using System.Collections;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlatformSizeInfo
    {
        public Sprite sprite;
        public Vector2 scale;
    }
    
    public class PlayerPlatform : MonoBehaviour
    {
        [SerializeField] private Transform _ballPointTransform;
        [SerializeField] private float _timeUntilCanLaunchBall = 0.1f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private PlatformSizeInfo[] _sizes;
        [SerializeField] private int _defaultSizeIndex;

        private MovableComponent _currentPinnedBall;
        private bool _canLaunchBall = true;

        public int DefaultSizeIndex => _defaultSizeIndex;

        public void SetPlatformSize(int index)
        {
            ChangePlatformSize(_sizes[index]);
        }

        private void ChangePlatformSize(PlatformSizeInfo sizeInfo)
        {
            transform.localScale = sizeInfo.scale;
            _spriteRenderer.sprite = sizeInfo.sprite;
            
            var spriteTransform = _spriteRenderer.gameObject.transform;
            spriteTransform.parent = null;
            spriteTransform.localScale = Vector2.one;
            spriteTransform.SetParent(transform);

            if (_currentPinnedBall != null)
            {
                var ballTransform = _currentPinnedBall.transform;
                ballTransform.parent = null;
                ballTransform.localScale = Vector2.one;
                ballTransform.SetParent(transform);
            }
        }
        
        public void PrepareBallToLaunch(MovableComponent ball)
        {
            if (_currentPinnedBall != null)
                LaunchBall();

            _currentPinnedBall = ball;
            var transform1 = _currentPinnedBall.transform;
            transform1.parent = _ballPointTransform;
            transform1.localPosition = Vector3.zero;
            _currentPinnedBall.PrepareToLaunch();

            StartCoroutine(TimeUntilLaunchBall());
        }

        public void LaunchBall()
        {
            if (_currentPinnedBall == null) return;
            if (_canLaunchBall == false) return;
            
            _currentPinnedBall.LaunchThisObject();
            _currentPinnedBall = null;
        }
        
        private IEnumerator TimeUntilLaunchBall()
        {
            _canLaunchBall = false;
            yield return new WaitForSeconds(_timeUntilCanLaunchBall);
            _canLaunchBall = true;
        }
    }
}
