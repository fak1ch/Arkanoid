using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.Singleton;
using ParserJsonSpace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.General.LocalizationSystemSpace
{
    public class LocalizationSystem : MonoSingleton<LocalizationSystem>
    {
        private const string JsonFolderName = @"Languages";
        private const string LanguageKeyPrefs = "Language";

        public event Action OnLanguageChanged;

        [SerializeField] private SystemLanguage _defaultLanguage;
        [SerializeField] private LanguageScriptableObject _languageSO;

        private SystemLanguage _currentLanguage;
        private static Dictionary<string, string> _languageDictionary;

        public List<SystemLanguage> Languages => _languageSO.languages;
        public SystemLanguage CurrentLanguage => _currentLanguage;

        protected override void Awake()
        {
            base.Awake();
            
            var saveLanguage = (SystemLanguage)PlayerPrefs.GetInt(
                LanguageKeyPrefs, (int)_defaultLanguage);
            SetLanguage(saveLanguage);
        }

        public void SetLanguage(SystemLanguage systemLanguage)
        {
            if (_currentLanguage == systemLanguage) return;
            _currentLanguage = systemLanguage;

            PlayerPrefs.SetInt(LanguageKeyPrefs, (int)_currentLanguage);
            
            LoadLanguageJsonByEnum(systemLanguage);
            OnLanguageChanged?.Invoke();
        }

        public string GetTextById(string id)
        {
            return _languageDictionary[id];
        }
        
        private void LoadLanguageJsonByEnum(SystemLanguage enumValue)
        {
            var jsonParser = new JsonParser<LanguageData>();
            enumValue = _languageSO.languages.Contains(enumValue) ? enumValue : _defaultLanguage;
            _languageDictionary = jsonParser.LoadDataFromFile($@"{JsonFolderName}\{enumValue}").languageDictionary;
        }
    }
}