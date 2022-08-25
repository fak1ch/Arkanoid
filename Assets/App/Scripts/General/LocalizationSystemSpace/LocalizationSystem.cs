using System;
using System.Collections.Generic;
using System.Linq;
using ParserJsonSpace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.General.LocalizationSystemSpace
{
    public class LocalizationSystem : MonoBehaviour
    {
        private const string JsonFolderName = @"Languages";
        private const string LanguageKeyPrefs = "Language";
        
        [SerializeField] private SystemLanguage _defaultLanguage;
        [SerializeField] private LanguageScriptableObject _languageSO;
        [SerializeField] private TMP_Dropdown _dropdown;

        [SerializeField] private SystemLanguage _currentLanguage;
        private static List<TranslatableText> _listId = new List<TranslatableText>();
        private static Dictionary<string, string> _languageDictionary;

        private void Awake()
        {
            _dropdown.options = new List<TMP_Dropdown.OptionData>();
            foreach (var l in _languageSO.languages)
            {
                var option = new TMP_Dropdown.OptionData()
                {
                    text = l.ToString()
                };
                
                _dropdown.options.Add(option);
            }
            
            var saveLanguage = (SystemLanguage)PlayerPrefs.GetInt(LanguageKeyPrefs, (int)_defaultLanguage);
            SetLanguage(saveLanguage);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += ClearListId;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded += ClearListId;
        }

        public void ChangeValueInDropdown()
        {
            for (int i = 0; i < _languageSO.languages.Count; i++)
            {
                if (_languageSO.languages[i].ToString() == _dropdown.options[_dropdown.value].text)
                {
                    SetLanguage(_languageSO.languages[i]);
                    return;
                }
            }
        }
        
        public void SetLanguage(SystemLanguage systemLanguage)
        {
            if (_currentLanguage == systemLanguage) return;
            _currentLanguage = systemLanguage;

            for (int i = 0; i < _dropdown.options.Count; i++)
            {
                if (_dropdown.options[i].text == systemLanguage.ToString())
                {
                    _dropdown.value = i;
                    break;
                }
            }
            
            PlayerPrefs.SetInt(LanguageKeyPrefs, (int)_currentLanguage);
            
            LoadLanguageJsonByEnum(systemLanguage);
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            foreach (var translatableText in _listId)
            {
                translatableText.SetText(_languageDictionary[translatableText.Id]);
            }
        }
        
        private void LoadLanguageJsonByEnum(SystemLanguage enumValue)
        {
            var jsonParser = new JsonParser<LanguageData>();
            enumValue = _languageSO.languages.Contains(enumValue) ? enumValue : _defaultLanguage;
            _languageDictionary = jsonParser.LoadDataFromFile($@"{JsonFolderName}\{enumValue}").languageDictionary;
        }

        public static void AddTextToList(TranslatableText text)
        {
            if (_languageDictionary != null)
                text.SetText(_languageDictionary[text.Id]);
            
            if (_listId.Any(translatableText => translatableText.Id == text.Id))
                return;

            _listId.Add(text);
        }
        
        
        private static void ClearListId(Scene arg0, LoadSceneMode arg1)
        {
            _listId.Clear();
        }
    }
}