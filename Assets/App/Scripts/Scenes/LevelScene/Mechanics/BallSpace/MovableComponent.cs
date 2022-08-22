using Player;
using System;
using Blocks;
using Pool;
using UnityEngine;
using Object = System.Object;

namespace BallSpace
{
    [Serializable]
    public class MovableInfo
    {
        public float speed;
        public Vector2 startDirection;
        public float plusMinusAngle;
        public int[] correctionAngles;

        [Space(10)]
        public Rigidbody2D rigidbody2D;
        public GameObject ballFuryTrigger;
    }

    public class MovableComponent : MonoBehaviour
    {
        [SerializeField] private MovableInfo _movableInfo;
        
        private Vector2 _velocityForReflection;
        private Vector2 _velocityUntilPauseGame;
        private bool _gameOnPause;

        public Rigidbody2D Rigidbody2D => _movableInfo.rigidbody2D;
        public float Speed
        {
            get => _movableInfo.speed;
            set
            {
                if (value >= 0)
                    _movableInfo.speed = value;
            }
        }
        public bool GameOnPause
        {
            get => _gameOnPause;
            set
            {
                _gameOnPause = value;

                if (value)
                {
                    _velocityUntilPauseGame = _movableInfo.rigidbody2D.velocity;
                    _movableInfo.rigidbody2D.velocity = Vector2.zero;
                }
                else
                {
                    _movableInfo.rigidbody2D.velocity = _velocityUntilPauseGame;
                    _velocityUntilPauseGame = _movableInfo.startDirection;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_gameOnPause == false)
            {
                NormalizeVelocity();
            }
            else
            {
                _movableInfo.rigidbody2D.velocity = Vector2.zero;
            }
        }

        public void PrepareToLaunch()
        {
            _movableInfo.rigidbody2D.isKinematic = true;
            _movableInfo.rigidbody2D.velocity = Vector2.zero;
        }

        public void LaunchThisObject()
        {
            transform.parent = null;
            _movableInfo.rigidbody2D.isKinematic = false;
            _movableInfo.rigidbody2D.velocity = _movableInfo.startDirection;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _movableInfo.rigidbody2D.velocity = Vector2.Reflect(_velocityForReflection, collision.contacts[0].normal);
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
            float angle = ConvertVectorToDegreeAngle(_movableInfo.rigidbody2D.velocity);

            for (int i = 0; i < _movableInfo.correctionAngles.Length; i++)
            {
                if (angle > _movableInfo.correctionAngles[i] - _movableInfo.plusMinusAngle
                    && angle < _movableInfo.correctionAngles[i] + _movableInfo.plusMinusAngle)
                {
                    float difference = _movableInfo.correctionAngles[i] - angle;
                    float newAngle;

                    if (difference >= 0)
                        newAngle = _movableInfo.correctionAngles[i] - _movableInfo.plusMinusAngle;
                    else
                        newAngle = _movableInfo.correctionAngles[i] + _movableInfo.plusMinusAngle;

                    _movableInfo.rigidbody2D.velocity = ConvertAngeToDirection(newAngle);
                }
            }
        }

        private void NormalizeVelocity()
        {
            var velocity = _movableInfo.rigidbody2D.velocity;
            velocity = velocity.normalized * _movableInfo.speed;
            _movableInfo.rigidbody2D.velocity = velocity;
            _velocityForReflection = velocity;
        }

        private float ConvertVectorToDegreeAngle(Vector2 vector)
        {
            return Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
        }

        private Vector2 ConvertAngeToDirection(float angle)
        {
            Vector2 direction = Vector2.zero;
            direction.y = (float)Math.Cos(angle / Mathf.Rad2Deg);
            direction.x = (float)Math.Sin(angle / Mathf.Rad2Deg);

            return direction;
        }

        public void SetBallFuryTriggerActive(bool value)
        {
            _movableInfo.ballFuryTrigger.SetActive(value);
        }
    }
}
