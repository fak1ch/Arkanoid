using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class ColorBomb : Block
    {
        [SerializeField] private Vector2[] _directions;
        private CellSelectableColorBomb _selectable;
        
        private void Start()
        {
            _selectable = new CellSelectableColorBomb(this, _blocks);
        }

        protected override void RunAdditionalLogic()
        {
            var blocksForDestroy = _selectable.GetBlocks(_directions);

            foreach (var b in blocksForDestroy)
            {
                b.DestroyBlock();
            }
        }
    }
}