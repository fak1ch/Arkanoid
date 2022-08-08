using Architecture;
using ColliderEvents;
using Player;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ball
{
    [Serializable]
    public class MovableSettings
    {
        public float startSpeed;
        public float maxSpeed;
        public Vector2 startDirection;
        public Vector2 correctionValues;
        public float plusMinusAngle;
        public int[] correctionAngles;

        [Space(10)]
        public Rigidbody2D rigidbody2D;
        public CollisionExit2DEvent collisionExit;
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
            _settings.rigidbody2D.velocity = _settings.startDirection;
            _settings.collisionExit.CollisionExit2D += CollisionExit2D;
        }

        public override void Tick()
        {
            NormalizeVelocity(_settings.rigidbody2D.velocity.normalized);
        }

        public override void CollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerPlatform playerPlatform) == false)
            {
                CorrectAngle();
            }
        }

        private void CorrectAngle()
        {
            float angle = Mathf.Atan2(_settings.rigidbody2D.velocity.y, _settings.rigidbody2D.velocity.x) * Mathf.Rad2Deg;

            int randomMultiplier = Random.Range(0, 2) == 0 ? 1 : -1;

            for(int i = 0; i < _settings.correctionAngles.Length; i++)
            {
                if (angle > _settings.correctionAngles[i] - _settings.plusMinusAngle 
                    && angle < _settings.correctionAngles[i] + _settings.plusMinusAngle)
                {
                    _settings.rigidbody2D.velocity += _settings.correctionValues * randomMultiplier;
                }
            }
        }

        private void NormalizeVelocity(Vector2 direction)
        {
            _settings.rigidbody2D.velocity = direction.normalized * Mathf.Lerp(_settings.startSpeed, _settings.maxSpeed, 1);
        }
    }
}
