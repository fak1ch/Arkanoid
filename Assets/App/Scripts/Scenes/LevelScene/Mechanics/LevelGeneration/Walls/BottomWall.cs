using BallSpace;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Walls
{
    public class BottomWall : MonoBehaviour
    {
        public event Action<MovableComponent> OnTriggerWithBall;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Vector2 _borders;
        private List<MovableComponent> _balls;
        private Vector2 _cameraUpperLeftAngle;
        private Vector2 _cameraBottomRightAngle;

        private void Start()
        {
            _cameraUpperLeftAngle = GetCoordsFromCameraViewport(new Vector2(0,0));
            _cameraBottomRightAngle = GetCoordsFromCameraViewport(new Vector2(1,1));
            
            _cameraUpperLeftAngle -= _borders;
            _cameraBottomRightAngle += _borders;
        }

        private void Update()
        {
            CheckBallsLeaveMap();
        }

        private void CheckBallsLeaveMap()
        {
            foreach (var ball in _balls)
            {
                if (IsBallOutOfBounds(ball.transform.position))
                {
                    OnTriggerWithBall?.Invoke(ball);
                    break;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Ball ball))
            {
                OnTriggerWithBall?.Invoke(ball.MovableComponent);
            }
        }

        public void SetCurrentBallsList(List<MovableComponent> balls)
        {
            _balls = balls;
        }
        
        private Vector2 GetCoordsFromCameraViewport(Vector2 viewportValues)
        {
            return _mainCamera.ViewportToWorldPoint(viewportValues);
        }

        private bool IsBallOutOfBounds(Vector2 ballPosition)
        {
            var x = ballPosition.x;
            var y = ballPosition.y;

            if (x < _cameraUpperLeftAngle.x || x > _cameraBottomRightAngle.x)
                return true;
            
            if (y < _cameraUpperLeftAngle.y || y > _cameraBottomRightAngle.y)
                return true;

            return false;
        }
    }
}
