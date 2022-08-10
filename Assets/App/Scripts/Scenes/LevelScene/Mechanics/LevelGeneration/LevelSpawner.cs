using Architecture;
using Blocks;
using ParserJson;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        private LevelSpawnerData _levelSpawnerData;

        private JsonParser<LevelData> _jsonParcer;
        private List<Block> _blocks = new List<Block>();

        public LevelSpawner(LevelSpawnerData settings)
        {
            _levelSpawnerData = settings;
        }

        public override void Initialize()
        {
            _jsonParcer = new JsonParser<LevelData>();
            LoadLevelDataFromJson();
        }

        public void BlockDestroy(Block block)
        {
            _blocks.Remove(block);
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

            for (int i = 0; i < _levelSpawnerData.levelData.BlocksCountRow * _levelSpawnerData.levelData.BlocksCountColumn; i++)
            {
                var block = Object.Instantiate(_levelSpawnerData.blockPrefab, _levelSpawnerData.blockContainer.transform);
                block.SetBoxColliderSize(newCellSize);
                _blocks.Add(block);
                block.OnBlockDestroy += BlockDestroy;
            }
        }

        public void DeleteAllBlocks()
        {
            if (_blocks.Count > 0)
            {
                for (int i = 0; i < _blocks.Count; i++)
                {
                    Object.Destroy(_blocks[i]);
                }
            }

            _blocks.Clear();
        }

        public void SaveCurrentLevelDataToJson()
        {
            _jsonParcer.SaveLevelDataToFile(_levelSpawnerData.levelData);
        }

        public void LoadLevelDataFromJson()
        {
            _levelSpawnerData.levelData = _jsonParcer.LoadLevelDataFromFile();

            CreateBlocks();
        }
    }
}
