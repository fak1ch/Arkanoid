using Architecture;
using Player;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BallSpace
{
    [Serializable]
    public class MovableInfo
    {
        public float speed;
        public Vector2 startDirection;
        public Vector2 correctionValues;
        public float plusMinusAngle;
        public int[] correctionAngles;

        [Space(10)]
        public Rigidbody2D rigidbody2D;
    }

    public class MovableComponent : MonoBehaviour
    {
        [SerializeField] private MovableInfo _movableInfo;

        private Vector2 _velocity;

        public Rigidbody2D Rigidbody2D => _movableInfo.rigidbody2D;

        public float Speed
        {
            get { return _movableInfo.speed; }
            set
            {
                if (value >= 0)
                    _movableInfo.speed = value;
            }
        }

        private void FixedUpdate()
        {
            NormalizeVelocity();
        }

        public void PrepareToLaunch()
        {
            _movableInfo.rigidbody2D.isKinematic = true;
        }

        public void LaunchThisObject(Vector2 direction)
        {
            transform.parent = null;
            _movableInfo.rigidbody2D.isKinematic = false;
            _movableInfo.startDirection = ClampVector2(direction + _movableInfo.startDirection);
            _movableInfo.rigidbody2D.velocity = _movableInfo.startDirection;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _movableInfo.rigidbody2D.velocity = Vector2.Reflect(_velocity, collision.contacts[0].normal);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerPlatform playerPlatform) == false)
            {
                CorrectAngle();
            }
        }

        private void CorrectAngle()
        {
            float angle = Mathf.Atan2(_movableInfo.rigidbody2D.velocity.y, _movableInfo.rigidbody2D.velocity.x) * Mathf.Rad2Deg;

            int randomMultiplier = Random.Range(0, 2) == 0 ? 1 : -1;

            for (int i = 0; i < _movableInfo.correctionAngles.Length; i++)
            {
                if (angle > _movableInfo.correctionAngles[i] - _movableInfo.plusMinusAngle
                    && angle < _movableInfo.correctionAngles[i] + _movableInfo.plusMinusAngle)
                {
                    _movableInfo.rigidbody2D.velocity += _movableInfo.correctionValues * randomMultiplier;
                }
            }
        }

        private void NormalizeVelocity()
        {
            _movableInfo.rigidbody2D.velocity = _movableInfo.rigidbody2D.velocity.normalized * _movableInfo.speed;
            _velocity = _movableInfo.rigidbody2D.velocity;
        }

        private Vector2 ClampVector2(Vector2 vector)
        {
            float max = Mathf.Max(vector.x, vector.y);

            vector.x /= max;
            vector.y /= max;

            return vector;
        }
    }
}
