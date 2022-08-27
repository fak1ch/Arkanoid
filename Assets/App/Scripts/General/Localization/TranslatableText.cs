using System;
using App.Scripts.General.SceneLoaderSpace;
using TMPro;
using UnityEngine;

namespace App.Scripts.General.LocalizationSystemSpace
{
    public class TranslatableText : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            LocalizationSystem.Instance.OnLanguageChanged += ChangeLanguage;
        }

        private void OnDisable()
        {
            if (LocalizationSystem.Instance == null) return;
            
            LocalizationSystem.Instance.OnLanguageChanged -= ChangeLanguage;
        }

        private void ChangeLanguage(SystemLanguage systemLanguage)
        {
            _text.text = LocalizationSystem.Instance.GetTextById(_id);
        }
        
        public void SetId(string text)
        {
            _id = text;
        }
    }
}