using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Blocks
{
    [Serializable]
    public class BlockData
    {
        public BoxCollider2D boxCollider;
        public Image blockImage;
        public Sprite blockSprite;
        public Color color;

        [Space(10)]
        public List<BlockHealthData> health;
        public Image blockBreakImage;
    }
}
