using System;
using UnityEngine;

namespace ColliderEvents
{
    public class TriggerExit2DEvent : MonoBehaviour
    {
        public Collider2D Collider { get; private set; }

        private void OnTriggerExit2D(Collider2D collider)
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

