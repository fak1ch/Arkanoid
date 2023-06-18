using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.Scenes.LevelCreatorSpace;
using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using Blocks;
using Blocks.BlockTypesSpace;
using LevelGeneration;
using ParserJsonSpace;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private CustomGridData _customGridData;
    [SerializeField] private float _gridLayoutTimeUntilActive;

    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _canvasMap;
    
    [SerializeField] private BlockScriptableObject _blocksInformation;
    [SerializeField] private GridLayoutGroupParent _blocksParent;

    private BlockContainer _blockContainer;
    private List<Block> _blocksPalette = new List<Block>();
    private Block _currentSelectedBlock;
    private CustomGrid _blockGrid;

    public int[][] GetConvertedGridToArray => _blockGrid.ConvertCurrentGridToArray();

    public void Initialize(int x, int y)
    {
        _customGridData.gridSize.x = x;
        _customGridData.gridSize.y = y;

        _blockContainer = new BlockContainer(_blocksInformation.blocks, _blocksParent.transform);
        _customGridData.blockContainer = _blockContainer;
        _blockGrid = new CustomGrid(_customGridData);
        InitializePalette(_blocksInformation.blocks, _blocksParent.transform, _blockContainer, _blocksPalette);

        StartCoroutine(SetGridLayoutGroupActiveRoutine(false, _gridLayoutTimeUntilActive));
    }

    public void LoadLevelData(LevelData levelData)
    {
        _blockGrid.InitializeGridByLevelData(levelData);
    }

    private void InitializePalette<T>(PoolObjectInformation<T>[] infos, Transform parent, 
        PoolContainer<T> poolContainer, List<T> listContainer) where T : MonoBehaviour
    {
        foreach (var info in infos)
        {
            var component = poolContainer.GetObjectFromPoolById(info.id);
            component.transform.SetParent(parent);
            component.gameObject.SetActive(true);
            listContainer.Add(component);
        }
    }
    
    private void Update()
    {
        if (_currentSelectedBlock != null)
            CheckGridParent();
        
        CheckPalette(ref _currentSelectedBlock, _blocksParent, _blocksPalette, _blockContainer);
    }

    private void CheckGridParent()
    {
        if (_customGridData.blockParent.PointerOverObject && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var indexes = _blockGrid.GetNearestIndexesBlockFromMap(mouseWorldPosition);

            if (_currentSelectedBlock != null)
            {
                _blockGrid.ReplaceBlock(indexes, _currentSelectedBlock);
                _currentSelectedBlock = null;
            }

            _customGridData.blockParent.PointerOverObject = false;
        }
    }
    
    private void CheckPalette<T>(ref T currentSelectedObject, GridLayoutGroupParent parent, 
        List<T> listPalette, PoolContainer<T> objectContainer) where T : MonoBehaviour, IInformation<T>
    {
        if (parent.PointerDownOverObject)
        {
            var component = GetNearestToMousePositionFromList(listPalette);
            currentSelectedObject = objectContainer.GetObjectFromPoolById(component.PoolObjectInformation.id);
            currentSelectedObject.transform.SetParent(_canvasMap);
            currentSelectedObject.gameObject.SetActive(true);
        }

        if (currentSelectedObject == null) return;
            
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            currentSelectedObject.transform.position = mouseWorldPosition;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            objectContainer.ReturnObjectToPool(currentSelectedObject, currentSelectedObject.PoolObjectInformation.id);
            currentSelectedObject.gameObject.SetActive(false);
            currentSelectedObject = null;
        }
    }
    
    private T GetNearestToMousePositionFromList<T>(List<T> list) where T : Component
    {
        Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        float min = Vector2.Distance(list[0].transform.position, mouseWorldPosition);
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            var distance = Vector2.Distance(list[i].transform.position, mouseWorldPosition);

            if (distance < min)
            {
                min = distance;
                index = i;
            }
        }

        return list[index];
    }
    
    private IEnumerator SetGridLayoutGroupActiveRoutine(bool value, float time)
    {
        yield return new WaitForSeconds(time);
        _blockGrid.SetGridLayoutGroupActive(value);
    }
}
