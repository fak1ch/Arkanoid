using System;
using UnityEngine;

namespace ColliderEvents
{
    public class TriggerEnter2DEvent : MonoBehaviour
    {
        public Collider2D Collider { get; private set; }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider != null)
            {
                Collider = collider;
            }
            else
            {
                Collider = null;
            }
        }
    }
}

