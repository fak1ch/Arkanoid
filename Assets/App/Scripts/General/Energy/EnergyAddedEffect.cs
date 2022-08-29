using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace App.Scripts.General.Energy
{
    public class EnergyAddedEffect : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _moveYOffset;
        [SerializeField] private float _moveYDuration;

        private void Start()
        {
            _text.DOFade(0, _fadeDuration);
            transform.DOMoveY(transform.position.y + _moveYOffset, _moveYDuration);
        }

        public void Initialize(string addedValue, Color color)
        {
            _text.text = addedValue;
            _text.color = color;
        }
    }
}