using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class BlockTNT : Block
    {
        [SerializeField] protected Vector2[] _directions;
        protected CellSelectable _cellSelectable;
        
        protected override void Start()
        {
            base.Start();
            _cellSelectable = new CellSelectable(this, blocks);
        }

        protected override void RunAdditionalLogic()
        {
            var blocksForDestroy = _cellSelectable.GetBlocks(_directions);

            foreach (var block in blocksForDestroy)
            {
                block.DestroyBlock();
            }
            
            blocksForDestroy.Clear();
        }
    }
}