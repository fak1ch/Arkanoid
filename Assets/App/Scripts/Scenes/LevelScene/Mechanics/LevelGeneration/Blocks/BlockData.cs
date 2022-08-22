using System;
using System.Collections.Generic;
using Blocks.BlockTypesSpace;
using UnityEngine;
using UnityEngine.UI;

namespace Blocks
{
    [Serializable]
    public class BlockData
    {
        public BoxCollider2D boxCollider;
        public Image blockImage;
        public RectTransform rectTransform;
        public Sprite blockSprite;

        [Space(10)]
        public int minHealth;
        public List<BlockHealthData> health;
        public Image blockBreakImage;
    }
}
