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
    }

    public class PlayerController : CustomBehaviour
    {
        private PlayerControllerInfo _playerControllerInfo;
        private InputSystem _inputSystem;

        public PlayerController(PlayerControllerInfo playerSettings, InputSystem inputSystem)
        {
            _playerControllerInfo = playerSettings;
            _inputSystem = inputSystem;
        }

        public override void FixedTick()
        {
            Vector2 newVelocity = _playerControllerInfo.rigidbody2D.velocity;
            newVelocity.x = _inputSystem.InputHorizontal * Time.fixedDeltaTime * _playerControllerInfo.speed;
            _playerControllerInfo.rigidbody2D.velocity = newVelocity;
        }
    }
}
