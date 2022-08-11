using Architecture;
using BallSpace;
using Blocks;
using ParserJson;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        private LevelSpawnerData _levelSpawnerData;

        private JsonParser<LevelData> _jsonParcer;
        private BallManager _ballManager;
        private List<List<Block>> _blocks;

        public LevelSpawner(LevelSpawnerData settings, BallManager ballManager)
        {
            _levelSpawnerData = settings;
            _ballManager = ballManager;
            _blocks = new List<List<Block>>(_levelSpawnerData.levelData.BlocksCountColumn);
        }

        public override void Initialize()
        {
            _jsonParcer = new JsonParser<LevelData>();

            LoadLevelDataFromJson(StaticLevelPath.LevelPath);
        }

        public void BlockDestroy(Block block)
        {
            _ballManager.DoJumpSpeedForAllBalls();
            block.OnBlockDestroy -= BlockDestroy;
            block.transform.localScale = new Vector3(0, 0, 0);
        }

        private Vector2 CalculateCellSize()
        {
            float newBlockWidth = Screen.width / _levelSpawnerData.canvas.scaleFactor;
            newBlockWidth -= _levelSpawnerData.blockContainer.padding.left + _levelSpawnerData.blockContainer.padding.right;
            newBlockWidth -= _levelSpawnerData.blockContainer.spacing.x * (_levelSpawnerData.levelData.BlocksCountRow - 1);
            newBlockWidth = newBlockWidth / _levelSpawnerData.levelData.BlocksCountRow;

            float procent = GetProcent(0, _levelSpawnerData.blockContainer.cellSize.x, newBlockWidth);

            return new Vector2(newBlockWidth, _levelSpawnerData.blockContainer.cellSize.y * procent);
        }

        public float GetProcent(float a, float b, float value)
        {
            if (Mathf.Approximately(b - a, 0))
                return 0;

            return (value - a) / (b - a);
        }

        private void CreateBlocks()
        {
            var newCellSize = CalculateCellSize();
            _levelSpawnerData.blockContainer.cellSize = newCellSize;

            DeleteAllBlocks();

            for (int i = 0; i < _levelSpawnerData.levelData.BlocksCountRow; i++)
            {
                _blocks.Add(new List<Block>(_levelSpawnerData.levelData.BlocksCountRow));
                for(int k = 0; k < _levelSpawnerData.levelData.BlocksCountColumn; k++)
                {
                    var block = Object.Instantiate(_levelSpawnerData.blockPrefab, _levelSpawnerData.blockContainer.transform);
                    block.SetBoxColliderSize(newCellSize);
                    _blocks[i].Add(block);
                    block.OnBlockDestroy += BlockDestroy;
                }
            }
        }

        public void DeleteAllBlocks()
        {
            if (_blocks.Count > 0)
            {
                for (int i = 0; i < _blocks.Count; i++)
                {
                    for(int k = 0; k < _blocks[i].Count; k++)
                    {
                        Object.Destroy(_blocks[i][k].gameObject);
                    }

                    _blocks[i].Clear();
                }
            }

            _blocks.Clear();
        }

        public void RecreateLevel()
        {
            CreateBlocks();
        }

        public void LoadLevelDataFromJson(string levelDataPath)
        {
            _levelSpawnerData.levelData = _jsonParcer.LoadLevelDataFromFile(levelDataPath);

            CreateBlocks();
        }
    }
}
