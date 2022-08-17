using System;
using System.Collections.Generic;
using System.Linq;
using LevelGeneration;
using Pool;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public enum BlockColors
    {
        None,
        Red,
        Green,
        Blue,
        Yellow,
    }
    
    public enum BlockTypes
    {
        ColorBlock,
        ImmortalBlock,
        StandartTNT,
        VerticalTNT,
        HorizontalTNT,
        ColorBomb,
    };

    public class BlockContainer : PoolContainer<Block>
    {
        private BlockInformation[] _blockList;

        public BlockContainer(BlockInformation[] blockList, Transform poolContainer) : base(blockList, poolContainer)
        {
            _blockList = blockList;
        }

        private BlockInformation GetBlockInfoById(int id)
        {
            return _blockList.FirstOrDefault(blockInfo => blockInfo.id == id);
        }

        public override Block GetObjectFromPoolById(int id)
        {
            var element = base.GetObjectFromPoolById(id);
            element.BlockInformation = GetBlockInfoById(id);

            return element;
        }
    }
}