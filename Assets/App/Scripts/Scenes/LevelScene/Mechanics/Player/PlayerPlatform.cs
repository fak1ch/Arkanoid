using BallSpace;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerPlatform : MonoBehaviour
    {
        [SerializeField] private Transform _ballPointTransform;
        [SerializeField] private float _timeUntilCanLaunchBall = 0.1f;

        private MovableComponent _currentPinnedBall;
        private bool _canLaunchBall = true;

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
