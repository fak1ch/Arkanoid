using Architecture;
using InputSystems;
using UnityEngine;
using System;

namespace Player
{
    [Serializable]
    public class PlayerSettings
    {
        public float speed;
        public Rigidbody2D rigidbody2D;
    }

    public class PlayerController : CustomBehaviour
    {
        private PlayerSettings _settings;
        private InputSystem _inputSystem;

        public PlayerController(PlayerSettings playerSettings, InputSystem inputSystem)
        {
            _settings = playerSettings;
            _inputSystem = inputSystem;
        }

        public override void FixedTick()
        {
            if (_inputSystem.InputHorizontal != 0)
            {
                Vector2 newPosition = _settings.rigidbody2D.position;
                newPosition.x += _inputSystem.InputHorizontal * Time.fixedDeltaTime * _settings.speed;
                _settings.rigidbody2D.MovePosition(newPosition);
            }
        }
    }
}
