using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class BlockTNT : Block
    {
        [SerializeField] private Vector2[] _directions;
        private List<Block> _blocksForDestroy;
        protected CellSelectable _cellSelectable;
        
        protected override void Start()
        {
            base.Start();
            _cellSelectable = new CellSelectable(this, blocks);
        }

        protected override void RunAdditionalLogic()
        {
            _blocksForDestroy = _cellSelectable.GetBlocks(_directions);

            foreach (var block in _blocksForDestroy)
            {
                block.DestroyBlock();
            }
            
            _blocksForDestroy.Clear();
        }
    }
}