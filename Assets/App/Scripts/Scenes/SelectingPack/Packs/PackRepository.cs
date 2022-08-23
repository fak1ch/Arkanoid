using System;
using LevelGeneration;
using ParserJsonSpace;
using UnityEngine;

namespace App.Scripts.Scenes.SelectingPack
{
    public class PackRepository
    {
        private const string LevelFileName = @"\Level";

        private int _id;
        private string _packName;
        private PackInformation _packInformation;
        
        private string _jsonPath;
        private JsonParser<LevelData> _jsonParser;
        
        private string _currentLevelIndexPlayerPrefsKey;
        private string _packCompleteKey;
        private int _currentLevelIndex;
        private int _levelCount = 0; 

        public bool IsComplete { get; private set; }
        public int CurrentLevelIndex => _currentLevelIndex;
        public int LevelCount => _levelCount;

        public PackRepository(PackInformation info)
        {
            _id = info.Id;
            _packName = info.Name;
            _packInformation = info;
            _jsonParser = new JsonParser<LevelData>();
            _jsonPath = $@"{Application.dataPath}\App\Resources\{_packName}";
            _currentLevelIndexPlayerPrefsKey = _packName + "CurrentLevelIndex";
            _packCompleteKey = _packName + "Complete";
            
            _levelCount = info.levelCount;
            _currentLevelIndex = PlayerPrefs.GetInt(_currentLevelIndexPlayerPrefsKey, 0);

            if (_currentLevelIndex > _levelCount)
                _levelCount = _currentLevelIndex;

            InitializePack();
        }

        private void InitializePack()
        {
            IsComplete = _currentLevelIndex == _levelCount;

            int value = PlayerPrefs.GetInt(_packCompleteKey, 0);
            IsComplete = value == 1;
        }

        public string GetLevelPath()
        {
            if (IsComplete) ClearProgress();
            
            StaticLevelPath.packId = _id;
            return $"{_packName}{LevelFileName}{_currentLevelIndex + 1}";
        }
         
        public string GetLevelPathByIndex(int index)
        {
            return $"{_packName}{LevelFileName}{index}";
        }
        
        public void LevelComplete()
        {
            _currentLevelIndex++;
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
            if (_jsonParser.SaveDataToFile(levelData, $"{_jsonPath}{LevelFileName}{_levelCount + 1}.json"))
            {
                _levelCount++;
            } 

            SaveIndexes();
        }
        
        public void ReplaceLevelInPack(LevelData levelData, int levelNumber)
        {
            _jsonParser.SaveDataToFile(levelData, $"{_jsonPath}{LevelFileName}{levelNumber}.json");
            SaveIndexes();
        }
        
        private void SaveIndexes() 
        {
            _packInformation.levelCount = _levelCount;
            PlayerPrefs.SetInt(_currentLevelIndexPlayerPrefsKey, _currentLevelIndex);
            PlayerPrefs.SetInt(_packCompleteKey, IsComplete ? 1 : 0);
        }
    }
}