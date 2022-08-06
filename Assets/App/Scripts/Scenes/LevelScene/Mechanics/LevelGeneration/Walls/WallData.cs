using System;
using UnityEngine;

namespace LevelGeneration
{
    [Serializable]
    public class WallData
    {
        public BoxCollider2D collider;
        public Vector2 scaleFactor;
    }
}
