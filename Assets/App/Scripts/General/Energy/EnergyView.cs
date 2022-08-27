using System;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using MathUtils = App.Scripts.General.Utils.MathUtils;

namespace App.Scripts.General.Energy
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _valuesText;
        [SerializeField] private Image _energyImage;
        [SerializeField] private TextMeshProUGUI _timerText;

        private int _currentEnergy;
        private int _maxEnergy;
        private bool _isEnergyAnimationWork = false;
        private Tween _changeEnergyTween;
        
        private void OnEnable()
        {
            _currentEnergy = EnergySystem.Instance.CurrentEnergy;
            ChangeEnergyView(_currentEnergy, EnergySystem.Instance.MaxEnergy);
            EnergySystem.Instance.OnEnergyValueChanged += ChangeEnergyView;
        }

        private void OnDisable()
        {
            if (EnergySystem.Instance == null) return;
            
            EnergySystem.Instance.OnEnergyValueChanged -= ChangeEnergyView;
        }

        private void Update()
        {
            if (_isEnergyAnimationWork)
            {
                _valuesText.text = $"{_currentEnergy}/{_maxEnergy}";
                var percent = MathUtils.GetPercent(0, _maxEnergy, _currentEnergy);
                _energyImage.fillAmount = Mathf.Clamp(percent, 0, 1);
            }
            
            string seconds = AddedZeroToStartString(EnergySystem.Instance.SecondsUntilAddEnergy.ToString());
            string minutes = AddedZeroToStartString(EnergySystem.Instance.MinutesUntilAddEnergy.ToString());
            _timerText.text = $"{minutes}:{seconds}";

        }

        private string AddedZeroToStartString(string str)
        {
            if (str.Length == 1)
            {
                return "0" + str;
            }

            return str;
        }
        
        private void ChangeEnergyView(int newEnergy, int maxEnergy)
        {
            _maxEnergy = maxEnergy;
            _isEnergyAnimationWork = true;
            
            _changeEnergyTween.Kill();
            _changeEnergyTween = DOTween.To(() => _currentEnergy, x => _currentEnergy = x, 
                    newEnergy, 1).OnComplete(() => _isEnergyAnimationWork = false);
        }
    }
}