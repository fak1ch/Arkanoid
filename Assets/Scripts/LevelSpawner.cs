using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private int _blocksCountRow;
    [SerializeField] private int _blocksCountColumn;

    [SerializeField] private GameObject _blockPrefab;
    [SerializeField] private GridLayoutGroup _blockContainer;
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        var newCellSize = CalculateCellSize();

        CreateBlocks(newCellSize);
    }

    private Vector2 CalculateCellSize()
    {
        float newBlockWidth = Screen.width / _canvas.scaleFactor;
        newBlockWidth -= _blockContainer.padding.left + _blockContainer.padding.right;
        newBlockWidth -= _blockContainer.spacing.x * (_blocksCountRow - 1);
        newBlockWidth = newBlockWidth / _blocksCountRow;

        float procent = Utils.GetProcent(0, _blockContainer.cellSize.x, newBlockWidth);

        return new Vector2(newBlockWidth, _blockContainer.cellSize.y * procent);
    }

    private void CreateBlocks(Vector2 newCellSize)
    {
        _blockContainer.cellSize = newCellSize;

        for (int i = 0; i < _blocksCountRow * _blocksCountColumn; i++)
        {
            var block = Instantiate(_blockPrefab, _blockContainer.transform);
        }
    }
}
