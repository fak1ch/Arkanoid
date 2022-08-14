using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class StandartTNT : Block
    {
        [SerializeField] private int _radius = 1;
        private List<Block> _blocksForDestroy = new List<Block>();
        private BlockGridHandler _blockGridHandler;

        private void Start()
        {
            _blockGridHandler = new BlockGridHandler(
                IndexColumn, IndexRow, _blocks, _blocksForDestroy);
        }

        protected override void RunAdditionalLogic()
        {
            for (int i = 1; i <= _radius; i++)
            {
                _blockGridHandler.MakeStepFromCurrentPosition(i, 0);
                _blockGridHandler.MakeStepFromCurrentPosition(i, -i);
                _blockGridHandler.MakeStepFromCurrentPosition(0, -i);
                _blockGridHandler.MakeStepFromCurrentPosition(-i, -i);
                _blockGridHandler.MakeStepFromCurrentPosition(-i, 0);
                _blockGridHandler.MakeStepFromCurrentPosition( -i, i);
                _blockGridHandler.MakeStepFromCurrentPosition(0, i);
                _blockGridHandler.MakeStepFromCurrentPosition(i, i);
            }

            for (int i = 0; i < _blocksForDestroy.Count; i++)
            {
                _blocksForDestroy[i].DestroyBlock();
            }
            
            _blocksForDestroy.Clear();
        }
    }
}