using BallSpace;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerPlatform : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform _ballPointTransform;

        private MovableComponent _currentPinnedBall;

        public void PrepareBallToLaunch(MovableComponent ball)
        {
            if (_currentPinnedBall != null)
                LaunchBall();  

            _currentPinnedBall = ball;
            _currentPinnedBall.Rigidbody2D.velocity = new Vector2(0, 0);
            _currentPinnedBall.transform.parent = _ballPointTransform;
            _currentPinnedBall.transform.localPosition = Vector3.zero;
            _currentPinnedBall.PrepareToLaunch();
        }

        private void LaunchBall()
        {
            _currentPinnedBall.LaunchThisObject(_rigidbody2D.velocity);
            _currentPinnedBall = null;
        }

        private void Update()
        {
            CheckActionToLaunchBall();
        }

        private void CheckActionToLaunchBall()
        {
            if (_currentPinnedBall != null)
            {
                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    LaunchBall();
                }
            }
        }
    }
}
