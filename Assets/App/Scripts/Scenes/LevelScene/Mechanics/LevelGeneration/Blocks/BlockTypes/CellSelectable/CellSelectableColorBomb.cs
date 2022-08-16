using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class CellSelectableColorBomb : CellSelectable
    {
        private List<Block> _blocksNeighbors = new List<Block>();
        private List<Block> _blocksForDestroy = new List<Block>();
        private Vector2[] _directions;
        
        public CellSelectableColorBomb(Block block, Block[][] blocks) : base(block, blocks)
        {
        }
        
        public override List<Block> GetBlocks(Vector2[] directions)
        {
            _directions = directions;

            int[][] moveMap = new int[width][];
            for (int i = 0; i < width; i++)
                moveMap[i] = new int[height];

            SearchColorBombNeighbors();

            int maxNeededColorBlocksCount = 0;
            for (int i = 0; i < _blocksNeighbors.Count; i++)
            {
                ResetMassiveToZero(moveMap);

                moveMap[blockX][blockY] = 1;
                BlockColors blockColor = _blocksNeighbors[i].Color;
                int neededColorBlocksCount = 0;

                while (true)
                {
                    int lastIterationBlocksCount = neededColorBlocksCount;
                    for (int k = 0; k < width; k++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (moveMap[k][j] == 1)
                            {
                                CheckNeighbor(blocks[k][j], blockColor, moveMap, ref neededColorBlocksCount);
                            }
                        }
                    }
                    
                    if (lastIterationBlocksCount == neededColorBlocksCount)
                        break;
                }

                if (neededColorBlocksCount > maxNeededColorBlocksCount)
                {
                    maxNeededColorBlocksCount = neededColorBlocksCount;
                    AddedAllBlocksToBlocksForDestroy(moveMap);
                }
            }

            return _blocksForDestroy;
        }
        
        
        private void AddedAllBlocksToBlocksForDestroy(int[][] moveMap)
        {
            _blocksForDestroy.Clear();
            
            for (int i = 0; i < moveMap.Length; i++)
            {
                for (int k = 0; k < moveMap[i].Length; k++)
                {
                    if (moveMap[i][k] == 2)
                    {
                        _blocksForDestroy.Add(blocks[i][k]);
                    }
                }
            }
        }

        private void CheckNeighbor(Block block, BlockColors neededColor, int[][] moveMap, ref int neededColorBlocksCount)
        {
            moveMap[block.IndexColumn][block.IndexRow] = 2;
            var cellSelectable = new CellSelectable(block, blocks);
            var blocksNeighbors = cellSelectable.GetBlocks(_directions);

            for (int i = 0; i < blocksNeighbors.Count; i++)
            {
                int cellValue = moveMap[blocksNeighbors[i].IndexColumn][blocksNeighbors[i].IndexRow];
                if (blocksNeighbors[i].Color == neededColor && cellValue == 0 && blocksNeighbors[i].IsDestroyed == false)
                {
                    moveMap[blocksNeighbors[i].IndexColumn][blocksNeighbors[i].IndexRow] = 1;
                    neededColorBlocksCount++;
                }
            }
        }
        
        private void ResetMassiveToZero(int[][] massive)
        {
            for (int i = 0; i < massive.Length; i++)
            {
                for (int k = 0; k < massive[i].Length; k++)
                {
                    massive[i][k] = 0;
                }
            }
        }

        private void SearchColorBombNeighbors()
        {
            var cellSelectable = new CellSelectable(thisBlock, blocks);
            _blocksNeighbors = cellSelectable.GetBlocks(_directions);
            
            var newBlocksNeighbors = new List<Block>();
            
            for (int i = 0; i < _blocksNeighbors.Count; i++)
            {
                if (_blocksNeighbors[i].BlockType == BlockTypes.ColorBlock)
                    newBlocksNeighbors.Add(_blocksNeighbors[i]);
            }
            
            _blocksNeighbors = new List<Block>(newBlocksNeighbors);
        }
    }
}