using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.Utils;
using Blocks;
using Blocks.BlockTypesSpace;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.LevelCreatorSpace
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
        private int _rowsCount;
        private int _columnCount;
        private Canvas _canvas;
        private GridLayoutGroupParent _blockParent;
        private BlockContainer _blockContainer;
        private Color _gridColor;

        private Block[][] _gridMap;
        
        public CustomGrid(CustomGridData customGridData)
        {
            _canvas = customGridData.canvas;
            _rowsCount = customGridData.gridSize.x;
            _columnCount = customGridData.gridSize.y;
            _blockParent = customGridData.blockParent;
            _gridColor = customGridData.gridColor;
            _blockContainer = customGridData.blockContainer;
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            _blockParent.gridLayoutGroup.cellSize = MapMathUtils.CalculateCellSize(
                _canvas, _blockParent.gridLayoutGroup, _columnCount);

            _gridMap = new Block[_rowsCount][];

            for (int i = 0; i < _gridMap.Length; i++)
            {
                _gridMap[i] = new Block[_columnCount];

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

        public int[][] ConvertCurrentGridToArray()
        {
            int[][] blocksMap = new int[_rowsCount][];

            for (int i = 0; i < blocksMap.Length; i++)
            {
                blocksMap[i] = new int[_columnCount];

                for (int k = 0; k < blocksMap[i].Length; k++)
                {
                    blocksMap[i][k] = _gridMap[i][k].BlockInformation.id;
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

        public void SetGridLayoutGroupActive(bool value)
        {
            _blockParent.gridLayoutGroup.enabled = value;
        }
    }
}