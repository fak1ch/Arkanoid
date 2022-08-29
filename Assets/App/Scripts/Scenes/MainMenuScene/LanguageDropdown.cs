using System;
using System.Collections.Generic;
using App.Scripts.General.LocalizationSystemSpace;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenuScene
{
    public class LanguageDropdown : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;

        private List<SystemLanguage> _languages;

        private void OnEnable()
        {
            LocalizationSystem.Instance.OnLanguageChanged += SetLanguageInDropDown;
        }

        private void OnDisable()
        {
            if (LocalizationSystem.Instance == null) return;
            LocalizationSystem.Instance.OnLanguageChanged -= SetLanguageInDropDown;
        }

        private void Awake()
        {
            InitializeDropdown();
        }

        private void InitializeDropdown()
        {
            _dropdown.options = new List<TMP_Dropdown.OptionData>();
            _languages = LocalizationSystem.Instance.Languages;
            foreach (var l in _languages)
            {
                var option = new TMP_Dropdown.OptionData()
                {
                    text = l.ToString()
                };
                
                _dropdown.options.Add(option);
            }
            
            SetLanguageInDropDown(LocalizationSystem.Instance.CurrentLanguage);
        }
        
        private void SetLanguageInDropDown(SystemLanguage language)
        {
            for (int i = 0; i < _dropdown.options.Count; i++)
            {
                if (_dropdown.options[i].text == language.ToString())
                {
                    _dropdown.value = i;
                    break;
                }
            }
        }
        
        public void DropDownValueChangedEvent()
        {
            for (int i = 0; i < _languages.Count; i++)
            {
                if (_languages[i].ToString() == _dropdown.options[_dropdown.value].text)
                {
                    LocalizationSystem.Instance.SetLanguage(_languages[i]);
                    return;
                }
            }
        }
    }
}
