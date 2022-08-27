using LevelGeneration;
using ParserJsonSpace;
using UnityEngine;
using System.IO;
using App.Scripts.General.PopUpSystemSpace;

namespace App.Scripts.Scenes.SelectingPack
{
    public class PackRepository
    {
        private const string LevelFileName = @"Level";

        private int _id;
        private string _packName;
        private PackInformation _packInformation;
        
        private string _jsonPathLoad;
        private string _jsonPathSave;
        private JsonParser<LevelData> _jsonParser;
        
        private string _currentLevelIndexPlayerPrefsKey;
        private string _packCompleteKey;
        private int _currentLevelIndex;
        private int _levelCount = 0; 

        public bool IsComplete { get; private set; }
        public bool CanOpenNextPack => PlayerPrefs.GetInt(_packCompleteKey) == 1;
        public int CurrentLevelIndex => _currentLevelIndex;
        public int LevelCount => _levelCount;
        public string Name => _packName;

        public PackRepository(PackInformation info)
        {
            _id = info.Id;
            _packName = info.Name;
            _packInformation = info;
            _jsonParser = new JsonParser<LevelData>();
            _jsonPathSave = Path.Combine(Application.dataPath, "App", "Resources", "Packs", 
                _packName, LevelFileName);
            _jsonPathLoad = Path.Combine("Packs", 
                _packName, LevelFileName);
            _currentLevelIndexPlayerPrefsKey = _packName + "CurrentLevelIndex";
            _packCompleteKey = _packName + "Complete";

            _levelCount = info.levelCount;
            _currentLevelIndex = PlayerPrefs.GetInt(_currentLevelIndexPlayerPrefsKey, 0);

            if (_currentLevelIndex > _levelCount)
                _currentLevelIndex = _levelCount;

            InitializePack();
        }

        private void InitializePack()
        {
            IsComplete = _currentLevelIndex == _levelCount;
        }

        public string GetLevelPath()
        {
            StaticLevelPath.packId = _id;
            return _jsonPathLoad + (_currentLevelIndex + 1);
        }
         
        public string GetLevelPathByIndex(int index)
        {
            return _jsonPathLoad + index;
        }
        
        public void LevelComplete()
        {
            _currentLevelIndex++;

            if (_currentLevelIndex > _levelCount)
                _currentLevelIndex = _levelCount;
            
            IsComplete = _currentLevelIndex == _levelCount;
            SaveIndexes();
        }

        public void ClearProgress()
        {
            _currentLevelIndex = 0;
            SaveIndexes();
        }
        
        public void AddLevelInPackToEnd(LevelData levelData)
        {
            if (_jsonParser.SaveDataToFile(levelData, _jsonPathSave + (_levelCount + 1) + ".json"))
            {
                _levelCount++;
                PopUpSystem.Instance.ShowInformationPopUp(
                    "Level saved in pack: " + _packName + " like " + LevelFileName + _levelCount + ".json");
            }
            else
            {
                PopUpSystem.Instance.ShowInformationPopUp("Error, level not was saved");
            }

            SaveIndexes();
        }
        
        public void ReplaceLevelInPack(LevelData levelData, int levelNumber)
        {
            if (_jsonParser.SaveDataToFile(levelData, _jsonPathSave + levelNumber + ".json"))
            {
                PopUpSystem.Instance.ShowInformationPopUp(
                    "Level was replaced: " + _packName + " like " + LevelFileName + _levelCount + ".json");
            }
            else
            {
                PopUpSystem.Instance.ShowInformationPopUp("Error, level not was replaced");
            }
            SaveIndexes();
        }
        
        private void SaveIndexes() 
        {
            _packInformation.levelCount = _levelCount;
            PlayerPrefs.SetInt(_currentLevelIndexPlayerPrefsKey, _currentLevelIndex);
            
            if (!CanOpenNextPack)
                PlayerPrefs.SetInt(_packCompleteKey, IsComplete ? 1 : 0);
        }
    }
}