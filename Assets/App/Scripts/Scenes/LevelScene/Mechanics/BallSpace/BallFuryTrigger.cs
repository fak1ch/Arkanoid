using System;
using Blocks;
using UnityEngine;

namespace BallSpace
{
    public class BallFuryTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Block block))
            {
                block.DestroyBlock();
            }
        }
    }
}