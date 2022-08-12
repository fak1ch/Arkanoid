using Architecture;
using Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BallSpace
{
    public class BallManager : CustomBehaviour
    {
        public event Action OnCurrentBallsZero;

        private readonly BallManagerData _data;
        private readonly ObjectPool<MovableComponent> _pool;
        private float _speed;

        private readonly List<MovableComponent> _currentBalls;

        public BallManager(BallManagerData data, ObjectPool<MovableComponent> pool)
        {
            _data = data;
            _pool = pool;
            _currentBalls = new List<MovableComponent>();
            _speed = _data.startBallSpeed;
        }

        public override void Initialize()
        {
            _pool.Initialize();
            _data.bottomWall.OnTriggerWithBall += DestroyBall;
            SetSpeedToAllBalls(_data.startBallSpeed);
            PlaceNewBallToPlayerPlatform();
        }

        public void PlaceNewBallToPlayerPlatform()
        {
            var ball = GetBallFromPool();
            _data.playerPlatform.PrepareBallToLaunch(ball);
            _currentBalls.Add(ball);
        }

        public void SpawnBallAtPositionWithDirection(Vector2 position, Vector2 moveDirection)
        {
            var ball = GetBallFromPool();
            ball.transform.position = position;
            ball.Rigidbody2D.velocity = moveDirection;
            _currentBalls.Add(ball);
        }

        private MovableComponent GetBallFromPool()
        {
            var ball = _pool.GetElement();
            ball.Speed = _speed;
            ball.gameObject.SetActive(true);

            return ball;
        }
        
        private void DestroyBall(MovableComponent ball)
        {
            ReturnBallToPool(ball);

            if (_currentBalls.Count == 0)
            {
                OnCurrentBallsZero?.Invoke();
                PlaceNewBallToPlayerPlatform();
            }
        }

        private void ReturnBallToPool(MovableComponent ball)
        {
            ball.gameObject.SetActive(false);
            ball.Speed = _data.startBallSpeed;
            ball.Rigidbody2D.velocity = Vector2.zero;
            
            _currentBalls.Remove(ball);
            _pool.ReturnElementToPool(ball);
        }
        
        public void DoJumpSpeedForAllBalls()
        {
            _speed = Mathf.Clamp(_speed + _data.speedJump, _data.startBallSpeed, _data.maxBallSpeed);

            foreach (var ball in _currentBalls)
            {
                ball.Speed = _speed;
            }
        }

        private void SetSpeedToAllBalls(float newSpeed)
        {
            foreach (var ball in _currentBalls)
            {
                ball.Speed = newSpeed;
            }
        }

        public void ReturnAllBallsToPool()
        {
            _speed = _data.startBallSpeed;
            
            for (int i = _currentBalls.Count - 1; i >= 0; i--)
            {
                ReturnBallToPool(_currentBalls[i]);
            }

            _currentBalls.Clear();
        }

        public void StopAllBalls()
        {
            foreach (var ball in _currentBalls)
            {
                ball.GameOnPause = true;
            }
        }

        public void BallsContinueMove()
        {
            foreach (var ball in _currentBalls)
            {
                ball.GameOnPause = false;
            }
        }
    }
}
