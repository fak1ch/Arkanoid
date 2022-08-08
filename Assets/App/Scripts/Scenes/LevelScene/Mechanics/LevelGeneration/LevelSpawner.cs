using Architecture;
using Blocks;
using ParcerJson;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        private LevelSpawnerData _settings;

        private JsonParcer<LevelData> _jsonParcer;
        private List<Block> _blocks = new List<Block>();

        public LevelSpawner(LevelSpawnerData settings)
        {
            _settings = settings;
        }

        public override void Initialize()
        {
            _jsonParcer = new JsonParcer<LevelData>();

            LoadLevelDataFromJson();
        }

        private Vector2 CalculateCellSize()
        {
            float newBlockWidth = Screen.width / _settings.canvas.scaleFactor;
            newBlockWidth -= _settings.blockContainer.padding.left + _settings.blockContainer.padding.right;
            newBlockWidth -= _settings.blockContainer.spacing.x * (_settings.levelData.BlocksCountRow - 1);
            newBlockWidth = newBlockWidth / _settings.levelData.BlocksCountRow;

            float procent = GetProcent(0, _settings.blockContainer.cellSize.x, newBlockWidth);

            return new Vector2(newBlockWidth, _settings.blockContainer.cellSize.y * procent);
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
            _settings.blockContainer.cellSize = newCellSize;

            DeleteAllBlocks();

            for (int i = 0; i < _settings.levelData.BlocksCountRow * _settings.levelData.BlocksCountColumn; i++)
            {
                var block = _settings.manipulator.Instantiate(_settings.blockPrefab, _settings.blockContainer.transform);
                block.SetBoxColliderSize(newCellSize);
                _blocks.Add(block);
            }
        }

        public void DeleteAllBlocks()
        {
            if (_blocks.Count > 0)
            {
                for (int i = 0; i < _blocks.Count; i++)
                {
                    _settings.manipulator.Destroy(_blocks[i]);
                }
            }

            _blocks.Clear();
        }

        public void SaveCurrentLevelDataToJson()
        {
            _jsonParcer.SaveLevelDataToFile(_settings.levelData);
        }

        public void LoadLevelDataFromJson()
        {
            _settings.levelData = _jsonParcer.LoadLevelDataFromFile();

            CreateBlocks();
        }
    }
}
