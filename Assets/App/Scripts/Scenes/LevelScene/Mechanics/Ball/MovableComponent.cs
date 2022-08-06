using Architecture;
using System;
using UnityEngine;

namespace Ball
{
    [Serializable]
    public class MovableSettings
    {
        public float speed;
        public Rigidbody2D rigidbody2D;
    }

    public class MovableComponent : CustomBehaviour
    {
        private MovableSettings _settings;

        public MovableComponent(MovableSettings settings)
        {
            _settings = settings;
        }

        public override void Initialize()
        {
            _settings.rigidbody2D.AddForce(new Vector2(100, 300));
        }
    }
}
