using System;
using UnityEngine;

namespace ColliderEvents
{
    public class CollisionExit2DEvent : MonoBehaviour
    {
        public event Action<Collision2D> CollisionExit2D;

        private void OnCollisionExit2D(Collision2D collision)
        {
            CollisionExit2D?.Invoke(collision);
        }
    }
}
