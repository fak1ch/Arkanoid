using Architecture;
using Pool;
using System;
using System.Collections;
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
        private bool _gameOnPause;
        private bool _ballOfFuryFlag;

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
            _data.bottomWall.SetCurrentBallsList(_currentBalls);
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
            ball.GameOnPause = _gameOnPause;
            ball.SetBallFuryTriggerActive(_ballOfFuryFlag);
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

            SetSpeedToAllBalls(_speed);
        }

        private void SetSpeedToAllBalls(float newSpeed)
        {
            _speed = Mathf.Clamp(newSpeed, _data.startBallSpeed, _data.maxBallSpeed);

            foreach (var ball in _currentBalls)
            {
                ball.Speed = _speed;
            }
        }

        public void AddValueToSpeedAllBalls(float addValueSpeed)
        {
            _speed += addValueSpeed;
            
            SetSpeedToAllBalls(_speed);
        }

        public void ReturnAllBallsToPool()
        {
            _speed = _data.startBallSpeed;

            while (_currentBalls.Count > 0)
            {
                ReturnBallToPool(_currentBalls[0]);
            }

            _currentBalls.Clear();
        }

        public void SetupBallInactive(bool value)
        {
            _gameOnPause = value;
            
            foreach (var ball in _currentBalls)
            {
                ball.GameOnPause = value;
            }
        }
        
        public void SetToAllBallsBallFuryFlag(bool value)
        {
            _ballOfFuryFlag = value;
            
            foreach (var ball in _currentBalls)
            {
                ball.SetBallFuryTriggerActive(value);
            }
        }

        public override void Dispose()
        {
            _data.bottomWall.OnTriggerWithBall -= DestroyBall;
        }
    }
}
