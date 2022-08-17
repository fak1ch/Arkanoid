using System;
using Architecture;
using BallSpace;
using Blocks;
using Blocks.BlockTypesSpace;
using UnityEngine;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        public event Action<Vector2> OnBlockDestroyed;
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

            _blocks = InitializeArrayInArray<Block>(_data.levelData.blocksIndexesArray.Length,
                _data.levelData.blocksIndexesArray[0].Length);
        }

        public override void Initialize()
        {
            _cellSize = CalculateCellSize();
            _data.blockContainer.cellSize = _cellSize;
            
            CreateBlocks();
            CalculateMaxBlocksCount();
        }

        private void BlockDestroy(Block block)
        {
            OnBlockDestroyed?.Invoke(block.transform.position);
            _ballManager.DoJumpSpeedForAllBalls();

            if (block.BlockInformation.type != BlockTypes.ImmortalBlock)
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
            block.transform.localScale = new Vector3(1, 1, 1);
            block.gameObject.SetActive(true);
        }

        private Vector2 CalculateCellSize()
        {
            float newBlockWidth = Screen.width / _data.canvas.scaleFactor;
            newBlockWidth -= _data.blockContainer.padding.left + _data.blockContainer.padding.right;
            newBlockWidth -= _data.blockContainer.spacing.x * (_data.levelData.blocksCountColumn - 1);
            newBlockWidth /= _data.levelData.blocksCountColumn;

            float percent = GetPercent(0, _data.blockContainer.cellSize.x, newBlockWidth);

            return new Vector2(newBlockWidth, _data.blockContainer.cellSize.y * percent);
        }

        private float GetPercent(float a, float b, float value)
        {
            if (Mathf.Approximately(b - a, 0))
                return 0;

            return (value - a) / (b - a);
        }

        private void CreateBlocks()
        {
            for (int i = 0; i < _data.levelData.blocksIndexesArray.Length; i++)
            {
                for(int k = 0; k < _data.levelData.blocksIndexesArray[i].Length; k++)
                {
                    var block = _blockContainer.GetObjectFromPoolById(_data.levelData.blocksIndexesArray[i][k]);
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
                    RestoreBlock(_blocks[i][k]);
                }
            }
        }

        private void RestoreBlock(Block block)
        {
            block.RestoreBlock();
        }

        private void CalculateMaxBlocksCount()
        {
            _maxBlocksCount = _data.levelData.Size;

            for (int i = 0; i < _blocks.Length; i++)
            {
                for (int k = 0; k < _blocks[i].Length; k++)
                {
                    if (_blocks[i][k].BlockInformation.type == BlockTypes.ImmortalBlock)
                    {
                        _maxBlocksCount--;
                    }
                }
            }

            _blocksCount = _maxBlocksCount;
        }

        private T[][] InitializeArrayInArray<T>(int width, int height)
        {
            var array = new T[width][];
            
            for (int i = 0; i < width; i++)
            {
                array[i] = new T[height];
            }

            return array;
        }
    }
}
