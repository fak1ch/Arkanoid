using Architecture;
using InputSystems;
using UnityEngine;
using System;

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
    }

    public class PlayerController : CustomBehaviour
    {
        private PlayerControllerInfo _data;
        private InputSystem _inputSystem;
        private bool _gameOnPause;
        private Vector2 _velocityUntilPauseGame;
        private Vector2 _startPosition;
        private float _speed;

        public bool GameOnPause
        {
            set
            {
                _gameOnPause = value;

                if (value)
                {
                    _velocityUntilPauseGame = _data.rigidbody2D.velocity;
                    _data.rigidbody2D.velocity = Vector2.zero;
                }
                else
                {
                    _data.rigidbody2D.velocity = _velocityUntilPauseGame;
                    _velocityUntilPauseGame = Vector2.zero;
                }
            }
        }

        public PlayerController(PlayerControllerInfo playerSettings, InputSystem inputSystem)
        {
            _data = playerSettings;
            _inputSystem = inputSystem;
            _inputSystem.OnButtonLaunchBallUnpressed += LaunchBall;
        }

        public override void Initialize()
        {
            _startPosition = _data.rigidbody2D.position;
            _speed = _data.startSpeed;
        }

        private void LaunchBall()
        {
            _data.playerPlatform.LaunchBall();
        }

        public void AddSpeed(float value)
        {
            SetSpeedToPlatform(_speed + value);
        }

        private void SetSpeedToPlatform(float newSpeed)
        {
            _speed = Mathf.Clamp(newSpeed, _data.minSpeed, _data.maxSpeed);
        }
        
        public override void FixedTick()
        {
            if (_gameOnPause == false)
            {
                Vector2 newVelocity = _data.rigidbody2D.velocity;
                newVelocity.x = _inputSystem.InputHorizontal * Time.fixedDeltaTime * _speed;
                _data.rigidbody2D.velocity = newVelocity;
            }
        }
        
        public void RestartPlayerPlatform()
        {
            _data.rigidbody2D.position = _startPosition;
        }

        public override void Dispose()
        {
            _inputSystem.OnButtonLaunchBallUnpressed += LaunchBall;
        }
    }
}
