using System;
using Architecture;
using BallSpace;
using Blocks;
using ParserJsonSpace;
using Blocks.BlockTypesSpace;
using Pool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        public event Action OnNoMoreBlocks;

        private LevelSpawnerData _data;
        private BallManager _ballManager;
        private Block[,] _blocks;

        private Vector2 _cellSize;
        private int _blocksCount;
        private int _maxBlocksCount;
        
        public LevelSpawner(LevelSpawnerData settings, BallManager ballManager)
        {
            _data = settings;
            _ballManager = ballManager;
            _blocks = new Block[_data.levelData.blocksCountColumn, _data.levelData.blocksCountRow];
        }

        public override void Initialize()
        {
            _cellSize = CalculateCellSize();
            _data.blockContainer.cellSize = _cellSize;

            CalculateMaxBlocksCount();
            CreateBlocks();
            
            //SaveCurrentMapToJson();
        }

        public void BlockDestroy(Block block)
        {
            _ballManager.DoJumpSpeedForAllBalls();

            _blocksCount--;
            if (_blocksCount < 0)
            {
                OnNoMoreBlocks?.Invoke();
            }
        }

        public Block CreateBlock(Block blockPrefab)
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
            var blockContainer = new BlockContainer(_data.blocksConfig);
            
            for (int i = 0; i < _data.levelData.blocksCountColumn; i++)
            {
                for(int k = 0; k < _data.levelData.blocksCountRow; k++)
                {
                    var blockPrefab = blockContainer.GetBlockByEnum(_data.levelData.blockTypes[i, k]);
                    var block = CreateBlock(blockPrefab);
                    block.SetBlocksMassive(_blocks, i, k);
                    _blocks[i, k] = block;
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
            for (int i = 0; i < _blocks.GetLength(0); i++)
            {
                for(int k = 0; k < _blocks.GetLength(1); k++)
                {
                    RestoreBlock(_blocks[i,k]);
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

            for (int i = 0; i < _data.levelData.blockTypes.GetLength(0); i++)
            {
                for (int k = 0; k < _data.levelData.blockTypes.GetLength(1); k++)
                {
                    if (_data.levelData.blockTypes[i, k] == BlockTypes.ImmortalBlock)
                    {
                        _maxBlocksCount--;
                    }
                }
            }

            _blocksCount = _maxBlocksCount;
        }
        
        private void SaveCurrentMapToJson()
        {
            _data.levelData.blockTypes = new BlockTypes[_blocks.GetLength(0), _blocks.GetLength(1)];
            
            for (int i = 0; i < _blocks.GetLength(0); i++)
            {
                for(int k = 0; k < _blocks.GetLength(1); k++)
                {
                    _data.levelData.blockTypes[i, k] = _blocks[i, k].BlockType;
                }
            }
            
            var jsonParser = new JsonParser<LevelData>();
            jsonParser.SaveLevelDataToFile(_data.levelData, StaticLevelPath.LevelPath);
        }
    }
}
