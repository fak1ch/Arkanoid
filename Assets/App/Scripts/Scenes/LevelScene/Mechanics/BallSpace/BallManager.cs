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

        private BallManagerData _data;
        private ObjectPool<MovableComponent> _pool;
        private float _speed;

        private List<MovableComponent> _currentBalls;

        public BallManager(BallManagerData data)
        {
            _data = data;
            _pool = new ObjectPool<MovableComponent>(_data.poolData.Size, _data.poolData.container, _data.ballPrefab);
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
            var ball = _pool.GetElement();
            _data.playerPlatform.PrepareBallToLaunch(ball);
            _currentBalls.Add(ball);
        }

        public void SpawnBallAtPositionWithDirection(Vector2 position, Vector2 moveDirection)
        {
            var ball = _pool.GetElement();
            ball.transform.parent = null;
            ball.transform.position = position;
            ball.Rigidbody2D.velocity = moveDirection;
            _currentBalls.Add(ball);
        }

        public void DestroyBall(MovableComponent ball)
        {
            if (_currentBalls.Contains(ball))
            {
                _currentBalls.Remove(ball);
            }

            _pool.ReturnElementToPool(ball);

            if (_currentBalls.Count == 0)
            {
                OnCurrentBallsZero?.Invoke();
                PlaceNewBallToPlayerPlatform();
            }
        }

        public void DoJumpSpeedForAllBalls()
        {
            _speed = Mathf.Clamp(_speed + _data.speedJump, _data.startBallSpeed, _data.maxBallSpeed);

            var balls = _pool.GetAllElementsFromPool();
            for(int i = 0; i < balls.Count; i++)
            {
                balls[i].Speed = _speed;
            }
        }

        private void SetSpeedToAllBalls(float newSpeed)
        {
            var balls = _pool.GetAllElementsFromPool();
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Speed = newSpeed;
            }
        }

        public void ReturnAllBallsToPool()
        {
            var balls = _pool.GetAllElementsFromPool();
            _speed = _data.startBallSpeed;

            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Speed = _speed;
                _pool.ReturnElementToPool(balls[i]);
            }

            _currentBalls.Clear();
        }

        public void StopAllBalls()
        {
            for(int i = 0; i < _currentBalls.Count; i++)
            {
                _currentBalls[i].GameOnPause = true;
            }
        }

        public void BallsContinueMove()
        {
            for (int i = 0; i < _currentBalls.Count; i++)
            {
                _currentBalls[i].GameOnPause = false;
            }
        }
    }
}
