using System;
using TMPro;
using UnityEngine;

namespace App.Scripts.General.LocalizationSystemSpace
{
    public class TranslatableText : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private TextMeshProUGUI _text;
        
        public string Id => _id;
        public string Text => _text.text;
        
        private void Start()
        {
            LocalizationSystem.AddTextToList(this);
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetId(string text)
        {
            _id = text;
        }
    }
}