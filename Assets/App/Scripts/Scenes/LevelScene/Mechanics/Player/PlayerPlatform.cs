using BallSpace;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerPlatform : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Transform _ballPointTransform;
        [SerializeField] private float _timeUntilCanLaunchBall = 0.1f;

        private MovableComponent _currentPinnedBall;
        private Vector2 _startPosition;
        private bool _canLaunchBall = true;

        private void Start()
        {
            _startPosition = transform.position;
        }

        public void PrepareBallToLaunch(MovableComponent ball)
        {
            if (_currentPinnedBall != null)
                LaunchBall();

            _currentPinnedBall = ball;
            _currentPinnedBall.transform.parent = _ballPointTransform;
            _currentPinnedBall.transform.localPosition = Vector3.zero;
            _currentPinnedBall.PrepareToLaunch();

            StartCoroutine(TimeUntilLaunchBall());
        }

        private IEnumerator TimeUntilLaunchBall()
        {
            _canLaunchBall = false;
            yield return new WaitForSeconds(_timeUntilCanLaunchBall);
            _canLaunchBall = true;
        }

        private void LaunchBall()
        {
            _currentPinnedBall.LaunchThisObject();
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
                if (Input.GetKeyUp(KeyCode.Mouse0) && _canLaunchBall == true)
                {
                    LaunchBall();
                }
            }
        }

        public void RestartPlayerPlatform()
        {
            transform.position = _startPosition;
        }
    }
}
