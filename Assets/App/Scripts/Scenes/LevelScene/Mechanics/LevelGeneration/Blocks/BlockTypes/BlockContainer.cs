using System;

namespace Blocks.BlockTypesSpace
{
    public enum BlockColors
    {
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

    public class BlockContainer
    {
        private BlockScriptableObject _blockList;
        
        public BlockContainer(BlockScriptableObject blockList)
        {
            _blockList = blockList;
        }

        public Block GetBlockById(int id)
        {
            Block block = null;
            
            foreach (var b in _blockList.blocks)
            {
                if (b.id == id)
                {
                    block = b.block;
                }
            }

            return block;
        }
    }
}