using Architecture;
using InputSystems;
using UnityEngine;
using System;
using System.Collections;
using App.Scripts.General.CoroutineManager;
using BallSpace;

namespace Player
{
    [Serializable]
    public class PlayerControllerInfo
    {
        public float startSpeed;
        public float minSpeed;
        public float maxSpeed;
        public Rigidbody2D rigidbody2D;
        public PlayerPlatform playerPlatform;
        public CoroutineManager coroutineManager;
    }

    public class PlayerController : CustomBehaviour
    {
        private PlayerControllerInfo _playerControllerInfo;
        private InputSystem _inputSystem;
        private bool _gameOnPause;
        private Vector2 _velocityUntilPauseGame;
        private Vector2 _startPosition;
        private float _speed;

        private Coroutine _speedBonusCoroutine;

        public bool GameOnPause
        {
            set
            {
                _gameOnPause = value;

                if (value)
                {
                    _velocityUntilPauseGame = _playerControllerInfo.rigidbody2D.velocity;
                    _playerControllerInfo.rigidbody2D.velocity = Vector2.zero;
                }
                else
                {
                    _playerControllerInfo.rigidbody2D.velocity = _velocityUntilPauseGame;
                    _velocityUntilPauseGame = Vector2.zero;
                }
            }
        }

        public PlayerController(PlayerControllerInfo playerSettings, InputSystem inputSystem)
        {
            _playerControllerInfo = playerSettings;
            _inputSystem = inputSystem;
            _inputSystem.OnButtonLaunchBallUnpressed += LaunchBall;
        }

        public override void Initialize()
        {
            _startPosition = _playerControllerInfo.rigidbody2D.position;
            _speed = _playerControllerInfo.startSpeed;
        }

        private void LaunchBall()
        {
            _playerControllerInfo.playerPlatform.LaunchBall();
        }

        public void AddSpeed(float value, float duration)
        {
            _speedBonusCoroutine = _playerControllerInfo.coroutineManager.StartCoroutine(SpeedBonusRoutine(value, duration));
        }

        private IEnumerator SpeedBonusRoutine(float addSpeedValue, float duration)
        {
            SetSpeedToPlatform(_speed + addSpeedValue);

            yield return new WaitForSeconds(duration);
            
            SetSpeedToPlatform(_playerControllerInfo.startSpeed);
        }
        
        private void SetSpeedToPlatform(float newSpeed)
        {
            _speed = Mathf.Clamp(newSpeed, _playerControllerInfo.minSpeed, _playerControllerInfo.maxSpeed);
        }
        
        public override void FixedTick()
        {
            if (_gameOnPause == false)
            {
                Vector2 newVelocity = _playerControllerInfo.rigidbody2D.velocity;
                newVelocity.x = _inputSystem.InputHorizontal * Time.fixedDeltaTime * _speed;
                _playerControllerInfo.rigidbody2D.velocity = newVelocity;
            }
        }
        
        public void RestartPlayerPlatform()
        {
            _playerControllerInfo.rigidbody2D.position = _startPosition;
        }
    }
}
