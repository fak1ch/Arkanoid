using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class ColorBomb : Block
    {
        private List<Block> _blocksNeighbors = new List<Block>();
        private List<Block> _blocksForDestroy = new List<Block>();
        private BlockGridHandler _blockGridHandler;

        private void Start()
        {
            _blockGridHandler = new BlockGridHandler(
                IndexColumn, IndexRow, _blocks, _blocksNeighbors);
        }

        protected override void RunAdditionalLogic()
        {
            int height = _blocks.GetLength(0);
            int width = _blocks.GetLength(1);
            int[,] moveMap = new int[height, width];

            SearchBlocksNeighbors();

            int maxNeededColorBlocksCount = 0;
            for (int i = 0; i < _blocksNeighbors.Count; i++)
            {
                ResetMassiveToZero(moveMap);
                
                moveMap[IndexColumn, IndexRow] = 1;
                BlockTypes blockType = _blocksNeighbors[i].BlockType;
                int neededColorBlocksCount = 0;

                while (true)
                {
                    int lastIterationBlocksCount = neededColorBlocksCount;
                    for (int k = 0; k < height; k++)
                    {
                        for (int j = 0; j < width; j++)
                        {
                            if (moveMap[k, j] == 1)
                            {
                                CheckNeighbor(_blocks[k,j], blockType, moveMap, ref neededColorBlocksCount);
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

            for (int i = 0; i < _blocksForDestroy.Count; i++)
            {
                _blocksForDestroy[i].DestroyBlock();
            }
        }

        private void AddedAllBlocksToBlocksForDestroy(int[,] moveMap)
        {
            _blocksForDestroy.Clear();
            
            for (int i = 0; i < moveMap.GetLength(0); i++)
            {
                for (int k = 0; k < moveMap.GetLength(1); k++)
                {
                    if (moveMap[i, k] == 2)
                    {
                        _blocksForDestroy.Add(_blocks[i,k]);
                    }
                }
            }
        }

        private void CheckNeighbor(Block block, BlockTypes neededType, int[,] moveMap, ref int neededColorBlocksCount)
        {
            moveMap[block.IndexColumn, block.IndexRow] = 2;
            
            var blocksNeighbors = new List<Block>();
            
            var blockGridHandler = new BlockGridHandler(
                block.IndexColumn, block.IndexRow, _blocks, blocksNeighbors);
            
            blockGridHandler.MakeStepFromCurrentPosition(0, 1);
            blockGridHandler.MakeStepFromCurrentPosition(0, -1);
            blockGridHandler.MakeStepFromCurrentPosition(1, 0);
            blockGridHandler.MakeStepFromCurrentPosition(-1, 0);

            for (int i = 0; i < blocksNeighbors.Count; i++)
            {
                if (blocksNeighbors[i].BlockType == neededType && moveMap[blocksNeighbors[i].IndexColumn, blocksNeighbors[i].IndexRow] == 0)
                {
                    moveMap[blocksNeighbors[i].IndexColumn, blocksNeighbors[i].IndexRow] = 1;
                    neededColorBlocksCount++;
                }
            }
        }
        
        private void ResetMassiveToZero(int[,] massive)
        {
            for (int i = 0; i < massive.GetLength(0); i++)
            {
                for (int k = 0; k < massive.GetLength(1); k++)
                {
                    massive[i, k] = 0;
                }
            }
        }

        private void SearchBlocksNeighbors()
        {
            _blockGridHandler.MakeStepFromCurrentPosition(0, 1);
            _blockGridHandler.MakeStepFromCurrentPosition(0, -1);
            _blockGridHandler.MakeStepFromCurrentPosition(1, 0);
            _blockGridHandler.MakeStepFromCurrentPosition(-1, 0);

            var newBlocksNeighbors = new List<Block>();
            
            for (int i = 0; i < _blocksNeighbors.Count; i++)
            {
                if (_blocksNeighbors[i].BlockType == BlockTypes.Red)
                    newBlocksNeighbors.Add(_blocksNeighbors[i]);
                if (_blocksNeighbors[i].BlockType == BlockTypes.Green)
                    newBlocksNeighbors.Add(_blocksNeighbors[i]);
                if (_blocksNeighbors[i].BlockType == BlockTypes.Blue)
                    newBlocksNeighbors.Add(_blocksNeighbors[i]);
                if (_blocksNeighbors[i].BlockType == BlockTypes.Yellow)
                    newBlocksNeighbors.Add(_blocksNeighbors[i]);
            }
            
            _blocksNeighbors = new List<Block>(newBlocksNeighbors);
        }
    }
}