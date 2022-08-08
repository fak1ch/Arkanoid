using System;
using UnityEngine;

namespace ColliderEvents
{
    public class CollisionEnter2DEvent : MonoBehaviour
    {
        public Collision2D Collision { get; private set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision != null)
            {
                Collision = collision;
            }
            else
            {
                Collision = null;
            }
        }
    }
}
