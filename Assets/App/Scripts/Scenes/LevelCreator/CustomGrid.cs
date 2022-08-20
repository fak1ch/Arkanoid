using System;
using System.Collections.Generic;
using System.Linq;
using Blocks;
using Blocks.BlockTypesSpace;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.LevelCreator
{
    [Serializable]
    public class CustomGridData
    {
        public Vector2Int gridSize;
        public GridLayoutGroupParent blockParent;
        public BlockContainer blockContainer;
        public Block emptyBlock;
        public Canvas canvas;
        public Color gridColor;
    }
    
    public class CustomGrid
    {
        private Vector2Int _gridSize;
        private Canvas _canvas;
        private GridLayoutGroupParent _blockParent;
        private BlockContainer _blockContainer;
        private Block _emptyBlock;
        private Color _gridColor;

        private Block[][] _gridMap;
        
        public CustomGrid(CustomGridData customGridData)
        {
            _canvas = customGridData.canvas;
            _gridSize = customGridData.gridSize;
            _blockParent = customGridData.blockParent;
            _emptyBlock = customGridData.emptyBlock;
            _gridColor = customGridData.gridColor;
            _blockContainer = customGridData.blockContainer;
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            _blockParent.gridLayoutGroup.cellSize = CalculateCellSize();
            Vector2 sizeDelta = _blockParent._rectTransform.sizeDelta;
            Vector2 cellSize = _blockParent.gridLayoutGroup.cellSize;
            var paddings = _blockParent.gridLayoutGroup.padding;
            float spacingY = _blockParent.gridLayoutGroup.spacing.y;
            float sizeDeltaY = cellSize.y * _gridSize.y + paddings.top + paddings.bottom + spacingY * _gridSize.y;
            _blockParent._rectTransform.sizeDelta = new Vector2(sizeDelta.x, sizeDeltaY);
            
            _gridMap = new Block[_gridSize.x][];

            for (int i = 0; i < _gridMap.Length; i++)
            {
                _gridMap[i] = new Block[_gridSize.y];

                for (int k = 0; k < _gridMap[i].Length; k++)
                {
                    CreateBlock(i,k);
                }
            }
        }

        public void ReplaceBlock(int[] indexes, Block newBlock)
        {
            int i = indexes[0];
            int k = indexes[1];
            newBlock.transform.position = _gridMap[i][k].transform.position;
            Object.Destroy(_gridMap[i][k].gameObject);
            _gridMap[i][k] = newBlock;
            _gridMap[i][k].transform.SetParent(_blockParent.transform);
            _gridMap[i][k].BlockData.rectTransform.sizeDelta = _blockParent.gridLayoutGroup.cellSize;
        }
        
        private void CreateBlock(int i, int k)
        {
            var emptyBlockId = _blockContainer.GetBlockInfoByType(BlockTypes.EmptyBlock).id;
            var block = _blockContainer.GetObjectFromPoolById(emptyBlockId);
            block.transform.SetParent(_blockParent.transform);
            block.BlockData.blockImage.color = _gridColor;
            _gridMap[i][k] = block;
            block.gameObject.SetActive(true);
        }

        public Block GetBlockFromMapByIndexes(int[] indexes)
        {
            return _gridMap[indexes[0]][indexes[1]];
        }

        public BlockJsonData[][] ConvertCurrentGridToArray()
        {
            BlockJsonData[][] blocksMap = new BlockJsonData[_gridSize.x][];

            for (int i = 0; i < blocksMap.Length; i++)
            {
                blocksMap[i] = new BlockJsonData[_gridSize.y];

                for (int k = 0; k < blocksMap[i].Length; k++)
                {
                    blocksMap[i][k] = new BlockJsonData
                    {
                        blockId = _gridMap[i][k].BlockInformation.id,
                        bonusId = _gridMap[i][k].BonusId
                    };
                }
            }

            return blocksMap;
        }
        
        public int[] GetNearestIndexesBlockFromMap(Vector2 mouseWorldPosition)
        {
            int[] blockIndexes = new int[2];

            float min = Vector2.Distance(_gridMap[0][0].transform.position, mouseWorldPosition);
            for (int i = 0; i < _gridMap.Length; i++)
            {
                for (int k = 0; k < _gridMap[i].Length; k++)
                {
                    var distance = Vector2.Distance(_gridMap[i][k].transform.position, mouseWorldPosition);
                    
                    if (distance < min)
                    {
                        min = distance;
                        blockIndexes[0] = i;
                        blockIndexes[1] = k;
                    }
                }
            }

            return blockIndexes;
        }
        
        private Vector2 CalculateCellSize()
        {
            float newBlockWidth = Screen.width / _canvas.scaleFactor;
            newBlockWidth -= _blockParent.gridLayoutGroup.padding.left + _blockParent.gridLayoutGroup.padding.right;
            newBlockWidth -= _blockParent.gridLayoutGroup.spacing.x * (_gridSize.x - 1);
            newBlockWidth /= _gridSize.x;

            float percent = GetPercent(0, _blockParent.gridLayoutGroup.cellSize.x, newBlockWidth);

            return new Vector2(newBlockWidth, _blockParent.gridLayoutGroup.cellSize.y * percent);
        }
    
        private float GetPercent(float a, float b, float value)
        {
            if (Mathf.Approximately(b - a, 0))
                return 0;

            return (value - a) / (b - a);
        }

        public void SetGridLayoutGroupActive(bool value)
        {
            _blockParent.gridLayoutGroup.enabled = value;
        }
    }
}