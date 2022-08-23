﻿using System;
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
        public Image bonusImage;
        public RectTransform rectTransform;

        [Space(10)]
        public List<Sprite> breakSprites;
        public Image blockBreakImage;
    }
}
