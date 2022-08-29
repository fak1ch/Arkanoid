using System;
using App.Scripts.General.Utils;
using Architecture;
using BallSpace;
using Blocks;
using Blocks.BlockTypesSpace;
using UnityEngine;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        public event Action<Block> OnBlockDestroyed;
        public event Action OnNoMoreBlocks;

        private LevelSpawnerData _data;
        private BallManager _ballManager;
        private BlockContainer _blockContainer;
        private Block[][] _blocks;

        private Vector2 _cellSize;
        private int _blocksCount;
        private int _maxBlocksCount;
        
        public LevelSpawner(LevelSpawnerData settings, BallManager ballManager, BlockContainer blockContainer)
        {
            _data = settings;
            _ballManager = ballManager;
            _blockContainer = blockContainer;
        }

        public override void Initialize()
        {
            _cellSize = MapMathUtils.CalculateCellSize(
                _data.canvas, _data.blockContainer, _data.levelData.BlocksCountRow);
            
            _data.blockContainer.cellSize = _cellSize;
            
            CreateBlocks();
            CalculateMaxBlocksCount();
        }

        private void BlockDestroy(Block block)
        {
            block.OnBlockDestroy -= BlockDestroy;
            OnBlockDestroyed?.Invoke(block);
            _ballManager.DoJumpSpeedForAllBalls();

            if (block.BlockInformation.type != BlockTypes.ImmortalBlock && block.BlockInformation.type != BlockTypes.EmptyBlock)
                _blocksCount--;

            if (_blocksCount == 0)
            {
                OnNoMoreBlocks?.Invoke();
            }
        }

        private void InitializeBlock(Block block, int indexX, int indexY)
        {
            block.SetBlocksMassive(_blocks, indexX, indexY);
            _blocks[indexX][indexY] = block;

            block.transform.SetParent(_data.blockContainer.transform);
            block.SetBoxColliderSize(_cellSize);
            block.OnBlockDestroy += BlockDestroy;
            block.gameObject.SetActive(true);
        }

        private void CreateBlocks()
        {
            _blocks = new Block[_data.levelData.BlocksCountColumn][];
            
            for (int i = 0; i < _blocks.Length; i++)
            {
                _blocks[i] = new Block[_data.levelData.BlocksCountRow];
                
                for(int k = 0; k < _blocks[i].Length; k++)
                {
                    var block = _blockContainer.GetObjectFromPoolById(_data.levelData.blocksMap[i][k]);
                    InitializeBlock(block, i, k);
                }
            }
        }

        public void RecreateLevel()
        {
            _blocksCount = _maxBlocksCount;
            RestoreAllBlocks();
        }

        private void RestoreAllBlocks()
        {
            for (int i = 0; i < _blocks.Length; i++)
            {
                for(int k = 0; k < _blocks[i].Length; k++)
                {
                    _blocks[i][k].RestoreBlock();
                    _blocks[i][k].OnBlockDestroy += BlockDestroy;
                }
            }
        }


        private void CalculateMaxBlocksCount()
        {
            _maxBlocksCount = _data.levelData.BlocksCountColumn * _data.levelData.BlocksCountRow;

            for (int i = 0; i < _blocks.Length; i++)
            {
                for (int k = 0; k < _blocks[i].Length; k++)
                {
                    if (_blocks[i][k].BlockInformation.type == BlockTypes.ImmortalBlock)
                    {
                        _maxBlocksCount--;
                    }
                    
                    if (_blocks[i][k].BlockInformation.type == BlockTypes.EmptyBlock)
                    {
                        _maxBlocksCount--;
                    }
                }
            }

            _blocksCount = _maxBlocksCount;
        }

        public void SetGameOnPauseFlagToBlocks(bool flag)
        {
            for (int i = 0; i < _blocks.Length; i++)
            {
                for(int k = 0; k < _blocks[i].Length; k++)
                {
                    _blocks[i][k].enabled = !flag;
                }
            }
        }
    }
}
