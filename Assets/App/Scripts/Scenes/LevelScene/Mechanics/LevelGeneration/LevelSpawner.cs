using System;
using Architecture;
using BallSpace;
using Blocks;
using ParserJsonSpace;
using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace LevelGeneration 
{
    public class LevelSpawner : CustomBehaviour
    {
        public event Action OnNoMoreBlocks;

        private LevelSpawnerData _data;
        private BallManager _ballManager;
        private ObjectPool<Block> _pool;
        private List<List<Block>> _blocks;

        private Vector2 _cellSize;
        private int _blockCount;
        
        public LevelSpawner(LevelSpawnerData settings, BallManager ballManager, ObjectPool<Block> pool)
        {
            _data = settings;
            _ballManager = ballManager;
            _pool = pool;
            _blocks = new List<List<Block>>(_data.levelData.blocksCountColumn);
            _blockCount = _data.levelData.Size;
        }

        public override void Initialize()
        {
            _cellSize = CalculateCellSize();
            _data.blockContainer.cellSize = _cellSize;

            _pool.Initialize();
            CreateBlocks();
        }

        public void BlockDestroy(Block block)
        {
            _ballManager.DoJumpSpeedForAllBalls();
            block.transform.localScale = new Vector3(0, 0, 0);
            block.OnBlockDestroy -= BlockDestroy;
            _pool.ReturnElementToPool(block);

            _blockCount--;
            if (_blockCount == 0)
            {
                OnNoMoreBlocks?.Invoke();
            }
        }

        public Block GetBlockFromPool()
        {
            var block = _pool.GetElement();
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
            newBlockWidth -= _data.blockContainer.spacing.x * (_data.levelData.blocksCountRow - 1);
            newBlockWidth /= _data.levelData.blocksCountRow;

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
            DeleteAllBlocks();

            for (int i = 0; i < _data.levelData.blocksCountRow; i++)
            {
                _blocks.Add(new List<Block>(_data.levelData.blocksCountRow));
                for(int k = 0; k < _data.levelData.blocksCountColumn; k++)
                {
                    var block = GetBlockFromPool();
                    _blocks[i].Add(block);
                }
            }
        }

        public void DeleteAllBlocks()
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                for(int k = 0; k < _blocks[i].Count; k++)
                {
                    BlockDestroy(_blocks[i][k]);
                }

                _blocks[i].Clear();
            }
            
            _blocks.Clear();
        }

        public void RecreateLevel()
        {
            RestoreAllBlocks();
        }

        private void RestoreAllBlocks()
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                for(int k = 0; k < _blocks[i].Count; k++)
                {
                    RestoreBlock(_blocks[i][k]);
                }
            }
        }

        private void RestoreBlock(Block block)
        {
            block.RestoreHealth();
            block.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
