using System;
using LevelGeneration;
using ParserJsonSpace;
using UnityEngine;

namespace App.Scripts.Scenes.SelectingPack
{
    [Serializable]
    public class PackRepositoryData
    {
        public int maxLevelIndex = 0;
    }
    
    public class PackRepository
    {
        private const string LevelFileName = @"\Level";
        private const string PackRepositoryFileName = @"\PackRepository";
        
        private int _id;
        private string _packName;
        
        private string _jsonPath;
        private JsonParser<LevelData> _jsonParser;
        
        private string _currentLevelIndexPlayerPrefsKey;
        private int _currentLevelIndex;
        private int _maxLevelIndex;

        public bool IsComplete { get; private set; }
        public int CurrentLevelIndex => _currentLevelIndex;
        public int MaxLevelIndex => _maxLevelIndex;

        public PackRepository(PackInformation info)
        {
            _id = info.Id;
            _packName = info.Name;
            _jsonParser = new JsonParser<LevelData>();
            _jsonPath = Application.dataPath + @"\App\Resources\" + _packName;
            _currentLevelIndexPlayerPrefsKey = _packName + "CurrentLevelIndex";

            var jsonParserPackRepository = new JsonParser<PackRepositoryData>();
            var data = jsonParserPackRepository.LoadDataFromFile(_packName + PackRepositoryFileName);
            _maxLevelIndex = data.maxLevelIndex;
            _currentLevelIndex = PlayerPrefs.GetInt(_currentLevelIndexPlayerPrefsKey, 0);

            InitializePack();
        }

        private void InitializePack()
        {
            IsComplete = _currentLevelIndex == _maxLevelIndex;
        }

        public string GetLevelPath()
        {
            StaticLevelPath.packId = _id;
            return _packName + LevelFileName + $"{_currentLevelIndex + 1}";
        }
        
        public void LevelComplete()
        {
            _currentLevelIndex++;
            SaveIndexes();
        }
        
        public void AddLevelInPackToEnd(LevelData levelData)
        {
            if (_jsonParser.SaveDataToFile(levelData, _jsonPath + LevelFileName + $"{_maxLevelIndex + 1}" + ".json"))
            {
                _maxLevelIndex++;
            }
            
            SaveIndexes();
        }
        
        public void ReplaceLevelInPack(LevelData levelData, int levelNumber)
        {
            _jsonParser.SaveDataToFile(levelData, _jsonPath + LevelFileName + $"{levelNumber}" + ".json");
            SaveIndexes();
        }
        
        private void SaveIndexes()
        {
            var jsonParserPackRepository = new JsonParser<PackRepositoryData>();
            var data = new PackRepositoryData()
            {
                maxLevelIndex = _maxLevelIndex,
            };
            jsonParserPackRepository.SaveDataToFile(data,_jsonPath + PackRepositoryFileName + ".json");
            _maxLevelIndex = data.maxLevelIndex;
            
            PlayerPrefs.SetInt(_currentLevelIndexPlayerPrefsKey, _currentLevelIndex);
        }
    }
}