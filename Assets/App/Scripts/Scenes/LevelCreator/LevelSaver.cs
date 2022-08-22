using System;
using System.Linq;
using App.Scripts.Scenes.SelectingPack;
using Blocks;
using LevelGeneration;
using ParserJsonSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.LevelCreatorSpace
{
    public class LevelSaver : MonoBehaviour
    {
        [SerializeField] private PackScriptableObject _packScriptableObject;
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private TMP_InputField _packIdInputField;
        [SerializeField] private TMP_InputField _levelIdForReplaceInputField;

        private LevelData ConvertCurrentMapToLevelData()
        {
            BlockJsonData[][] blocksMap = _levelCreator.GetConvertedGridToArray;
        
            LevelData levelData = new LevelData
            {
                blocksMap = blocksMap
            };

            return levelData;
        }

        public void AddLevelInPackToEnd()
        {
            var levelData = ConvertCurrentMapToLevelData();
            
            var packRepository = new PackRepository(GetPackInfoById(Convert.ToInt32(_packIdInputField.text)));
            packRepository.AddLevelInPackToEnd(levelData);
        }

        public void ReplaceLeveInPackByIndex()
        {
            var levelData = ConvertCurrentMapToLevelData();
            
            var packRepository = new PackRepository(GetPackInfoById(Convert.ToInt32(_packIdInputField.text)));
            packRepository.ReplaceLevelInPack(levelData, Convert.ToInt32(_levelIdForReplaceInputField.text));
        }

        public void SetActivePanel(bool value)
        {
            gameObject.SetActive(value);
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}