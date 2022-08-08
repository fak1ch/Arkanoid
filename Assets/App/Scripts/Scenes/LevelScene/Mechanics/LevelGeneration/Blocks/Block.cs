using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boxCollider;

        public void SetBoxColliderSize(Vector2 newSize)
        {
            _boxCollider.size = newSize;
        }
    }
}
