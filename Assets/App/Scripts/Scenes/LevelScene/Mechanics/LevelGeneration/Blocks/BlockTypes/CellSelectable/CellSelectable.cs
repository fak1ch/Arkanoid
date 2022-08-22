using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class CellSelectable
    {
        protected readonly int blockX;
        protected readonly int blockY;
        protected readonly int width;
        protected readonly int height;
        protected readonly Block[][] blocks;
        protected readonly Block thisBlock;
        protected readonly List<Block> blocksForDestroy;

        public CellSelectable(Block block, Block[][] blocks)
        {
            blockX = block.IndexColumn;
            blockY = block.IndexRow;
            thisBlock = block;
            this.blocks = blocks;
            blocksForDestroy = new List<Block>();
            width = this.blocks.Length;
            height = this.blocks[0].Length;
        }

        public virtual List<Block> GetBlocks(Vector2[] directions)
        {
            foreach (var dir in directions)
            {
                MakeStepFromCurrentPosition(dir);
            }

            return blocksForDestroy;
        }

        protected void MakeStepFromCurrentPosition(Vector2 direction)
        {
            int newIndexX = blockX + (int)direction.x;
            int newIndexY = blockY + (int)direction.y;

            if (newIndexX < 0 || newIndexX > width - 1)
                return;
            if (newIndexY < 0 || newIndexY > height - 1)
                return;

            blocksForDestroy.Add(blocks[newIndexX][newIndexY]);
        }
    }
}