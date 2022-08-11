using BallSpace;
using System;
using UnityEngine;

namespace Walls
{
    public class BottomWall : MonoBehaviour
    {
        public event Action<MovableComponent> OnTriggerWithBall;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Ball ball))
            {
                ball.PlayDestroyEffect();
                OnTriggerWithBall?.Invoke(ball.MovableComponent);
            }
        }
    }
}
