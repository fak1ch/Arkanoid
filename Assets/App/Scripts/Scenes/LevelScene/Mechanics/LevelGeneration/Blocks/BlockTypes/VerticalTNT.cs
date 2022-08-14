using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class VerticalTNT : Block
    {
        private List<Block> _blocksForDestroy = new List<Block>();
        private BlockGridHandler _blockGridHandler;
        
        private void Start()
        {
            _blockGridHandler = new BlockGridHandler(
                IndexColumn, IndexRow, _blocks, _blocksForDestroy);
        }

        protected override void RunAdditionalLogic()
        {
            int k = 1;
            
            while (true)
            {
                var topBlock = _blockGridHandler.MakeStepFromCurrentPosition(k, 0);
                var bottomBlock = _blockGridHandler.MakeStepFromCurrentPosition(-k, 0);
                
                if (topBlock == null && bottomBlock == null)
                    break;

                k++;
            }

            for (int i = 0; i < _blocksForDestroy.Count; i++)
            {
                _blocksForDestroy[i].DestroyBlock();
            }
            
            _blocksForDestroy.Clear();
        }
    }
}