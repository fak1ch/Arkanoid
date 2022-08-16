using System;
using Architecture;
using BallSpace;
using Blocks;
using Blocks.BlockTypesSpace;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        public event Action OnNoMoreBlocks;

        private LevelSpawnerData _data;
        private BallManager _ballManager;
        private BlockContainer _blockContainer;
        private Block[][] _blocks;

        private Vector2 _cellSize;
        private int _blocksCount;
        private int _maxBlocksCount;
        
        public LevelSpawner(LevelSpawnerData settings, BallManager ballManager)
        {
            _data = settings;
            _ballManager = ballManager;
            _blockContainer = new BlockContainer(_data.blocksConfig);

            _blocks = InitializeArrayInArray<Block>(_data.levelData.blocksIndexesArray.Length,
                _data.levelData.blocksIndexesArray[0].Length);
        }

        public override void Initialize()
        {
            _cellSize = CalculateCellSize();
            _data.blockContainer.cellSize = _cellSize;

            CalculateMaxBlocksCount();
            CreateBlocks();
        }

        private void BlockDestroy(Block block)
        {
            _ballManager.DoJumpSpeedForAllBalls();

            if (block.BlockType != BlockTypes.ImmortalBlock)
                _blocksCount--;

            if (_blocksCount == 0)
            {
                OnNoMoreBlocks?.Invoke();
            }
        }

        private Block CreateBlock(Block blockPrefab)
        {
            var block = Object.Instantiate(blockPrefab, _data.blockContainer.transform);
            
            block.SetBoxColliderSize(_cellSize);
            block.OnBlockDestroy += BlockDestroy;
            block.transform.localScale = new Vector3(1, 1, 1);
            block.gameObject.SetActive(true);

            return block;
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
                    var blockPrefab = _blockContainer.GetBlockById(_data.levelData.blocksIndexesArray[i][k]);
                    var block = CreateBlock(blockPrefab);
                    block.SetBlocksMassive(_blocks, i, k);
                    _blocks[i][k] = block;
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

            for (int i = 0; i < _data.levelData.blocksIndexesArray.Length; i++)
            {
                for (int k = 0; k < _data.levelData.blocksIndexesArray[i].Length; k++)
                {
                    int id = _data.levelData.blocksIndexesArray[i][k];
                    
                    if (_blockContainer.GetBlockById(id).BlockType == BlockTypes.ImmortalBlock)
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
