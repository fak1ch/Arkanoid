using Architecture;
using Pool;
using UnityEngine;

namespace BallSpace
{
    public class BallManager : CustomBehaviour
    {
        private BallManagerData _data;
        private ObjectPool<MovableComponent> _pool;
        private float _speed;

        public BallManager(BallManagerData data)
        {
            _data = data;
            _pool = new ObjectPool<MovableComponent>(_data.poolData.Size, _data.poolData.container, _data.ballPrefab);
            _speed = _data.startBallSpeed;
        }

        public override void Initialize()
        {
            _pool.Initialize();
            SetSpeedToAllBalls(_data.startBallSpeed);
            PlaceNewBallToPlayerPlatform();
        }

        public void PlaceNewBallToPlayerPlatform()
        {
            var ball = _pool.GetElement();
            _data.playerPlatform.PrepareBallToLaunch(ball);
        }

        public void SpawnBallAtPositionWithDirection(Vector2 position, Vector2 moveDirection)
        {
            var ball = _pool.GetElement();
            ball.transform.parent = null;
            ball.transform.position = position;
            ball.Rigidbody2D.velocity = moveDirection;
        }

        public void DestroyBall(MovableComponent ball)
        {
            _pool.ReturnElementToPool(ball);
        }

        public void DoJumpSpeedForAllBalls()
        {
            _speed = Mathf.Clamp(_speed + _data.speedJump, _data.startBallSpeed, _data.maxBallSpeed);

            var balls = _pool.GetAllElementsFromPool();
            for(int i = 0; i < balls.Count; i++)
            {
                balls[i].Speed = _speed;
            }
        }

        private void SetSpeedToAllBalls(float newSpeed)
        {
            var balls = _pool.GetAllElementsFromPool();
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Speed = newSpeed;
            }
        }

        public void ReturnAllBallsToPool()
        {
            var balls = _pool.GetAllElementsFromPool();
            for (int i = 0; i < balls.Count; i++)
            {
                _pool.ReturnElementToPool(balls[i]);
            }
        }
    }
}
