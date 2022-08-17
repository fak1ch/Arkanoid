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
        private Coroutine _speedBonusCoroutine;
        private Coroutine _ballFuryCorutine;

        private float _speedUntilBoost;
        private float _speedJumpsValue;
        private int _blockLayerId;
        private int _ballLayerId;

        private readonly List<MovableComponent> _currentBalls;

        public BallManager(BallManagerData data, ObjectPool<MovableComponent> pool)
        {
            _data = data;
            _pool = pool;
            _currentBalls = new List<MovableComponent>();
            _speed = _data.startBallSpeed;
            _blockLayerId = (int)Mathf.Log(_data.blockLayer.value, 2);
            _ballLayerId = (int)Mathf.Log(_data.ballLayer.value, 2);
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
            _speedJumpsValue += _data.speedJump;
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
            foreach (var ball in _currentBalls)
            {
                ball.GameOnPause = value;
            }
        }

        public void ChangeBallsSpeedForTime(float addSpeedValue, float seconds)
        {
            if (_speedBonusCoroutine == null)
                _speedUntilBoost = _speed;
            else 
                _data.coroutineManager.StopCoroutine(_speedBonusCoroutine);
            
            _speedBonusCoroutine = _data.coroutineManager.StartCoroutine(SpeedBonusRoutine(addSpeedValue, seconds));
        }
        
        public void ActivateBonusBallOfFury(float duration)
        {
            if (_ballFuryCorutine != null)
                _data.coroutineManager.StopCoroutine(_ballFuryCorutine);
            
            _ballFuryCorutine = _data.coroutineManager.StartCoroutine(BallOfFuryBonusRoutine(duration));
        }
        
        private IEnumerator SpeedBonusRoutine(float addSpeedValue, float duration)
        {
            SetSpeedToAllBalls(_speed + addSpeedValue);
            _speedJumpsValue = 0;

            yield return new WaitForSeconds(duration);
            
            SetSpeedToAllBalls(_speedUntilBoost + _speedJumpsValue);
            _speedBonusCoroutine = null;
        }
        
        private IEnumerator BallOfFuryBonusRoutine(float duration)
        {
            SetToAllBallsBallFuryFlag(true);
            SetIgnoreLayerCollision(true);
            
            yield return new WaitForSeconds(duration);
            
            SetIgnoreLayerCollision(false);
            SetToAllBallsBallFuryFlag(false);
            _ballFuryCorutine = null;
        }

        private void SetIgnoreLayerCollision(bool value)
        {
            Physics2D.IgnoreLayerCollision(_blockLayerId, _ballLayerId, value);
        }

        private void SetToAllBallsBallFuryFlag(bool value)
        {
            foreach (var ball in _currentBalls)
            {
                ball.SetBallFuryTriggerActive(value);
            }
        }
    }
}
