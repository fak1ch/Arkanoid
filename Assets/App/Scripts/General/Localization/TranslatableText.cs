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
            LocalizationSystem.Instance.OnLanguageChanged += UpdateLanguage;
        }

        private void OnDisable()
        {
            if (LocalizationSystem.Instance == null) return;
            
            LocalizationSystem.Instance.OnLanguageChanged -= UpdateLanguage;
        }

        private void Start()
        {
            UpdateLanguage();
        }

        private void UpdateLanguage()
        {
            _text.text = LocalizationSystem.Instance.GetTextById(_id);
        }
        
        public void SetId(string text)
        {
            _id = text;
            UpdateLanguage();
        }
    }
}