using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Architecture
{
    public abstract class CustomBehaviour
    {
        public virtual void Initialize() { }

        public virtual void Tick() { }
        public virtual void FixedTick() { }

        public virtual void CollisionEnter2D(Collision2D collision) { }
        public virtual void CollisionExit2D(Collision2D collision) { }
        public virtual void TriggerEnter2D(Collider2D collider) { }
        public virtual void TriggerExit2D(Collider2D collider) { }

        public virtual void Dispose() { }
    }
}
