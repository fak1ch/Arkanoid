using Architecture;
using InputSystems;
using UnityEngine;
using System;
using BallSpace;

namespace Player
{
    [Serializable]
    public class PlayerControllerInfo
    {
        public float speed;
        public Rigidbody2D rigidbody2D;
        public PlayerPlatform playerPlatform;
    }

    public class PlayerController : CustomBehaviour
    {
        private PlayerControllerInfo _playerControllerInfo;
        private InputSystem _inputSystem;
        private bool _gameOnPause;
        private Vector2 _velocityUntilPauseGame;
        private Vector2 _startPosition;

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
        }

        private void LaunchBall()
        {
            _playerControllerInfo.playerPlatform.LaunchBall();
        }

        public override void FixedTick()
        {
            if (_gameOnPause == false)
            {
                Vector2 newVelocity = _playerControllerInfo.rigidbody2D.velocity;
                newVelocity.x = _inputSystem.InputHorizontal * Time.fixedDeltaTime * _playerControllerInfo.speed;
                _playerControllerInfo.rigidbody2D.velocity = newVelocity;
            }
        }
        
        public void RestartPlayerPlatform()
        {
            _playerControllerInfo.rigidbody2D.position = _startPosition;
        }
    }
}
