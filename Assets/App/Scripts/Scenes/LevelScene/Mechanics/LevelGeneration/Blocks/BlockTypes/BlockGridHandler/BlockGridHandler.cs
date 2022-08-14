using System.Collections.Generic;

namespace Blocks.BlockTypesSpace
{
    public class BlockGridHandler
    {
        private int _blockX;
        private int _blockY;
        private Block[,] _blocks;
        private List<Block> _blocksForDestroy = new List<Block>();
        
        public BlockGridHandler(int currentBlockX, int currentBlockY, Block[,] blocks, List<Block> blockForDestroy)
        {
            _blockX = currentBlockX;
            _blockY = currentBlockY;
            _blocks = blocks;
            _blocksForDestroy = blockForDestroy;
        }
        
        public Block MakeStepFromCurrentPosition(int x, int y)
        {
            int blockX = _blockX + x;
            int blockY = _blockY + y;

            if (!CheckCorrectIndex(blockX, 0, _blocks.GetLength(0) - 1))
                return null;
            
            if (!CheckCorrectIndex(blockY, 0, _blocks.GetLength(1) - 1))
                return null;

            _blocksForDestroy.Add(_blocks[blockX, blockY]);
            
            return _blocks[blockX, blockY]; 
        }

        private bool CheckCorrectIndex(int index, int minIndex, int maxIndex)
        {
            if (index < minIndex || index > maxIndex)
                return false;

            return true;
        }
    }
}