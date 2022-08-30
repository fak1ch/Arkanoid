using System;
using System.Globalization;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.Singleton;
using UnityEngine;

namespace App.Scripts.General.Energy
{
    public class EnergySystem : MonoSingleton<EnergySystem>
    {
        private const string ExitTimeKey = "ExitTime";
        private const string CurrentEnergyKey = "CurrentEnergy";

        public event Action<int, int> OnEnergyValueChanged; 

        [SerializeField] private int _maxEnergy;
        [SerializeField] private int _startLevelPrice;
        [SerializeField] private int _addForPassingLevel;
        [SerializeField] private string _infoTranslateId;

        [Space(10)]
        [SerializeField] private int _minutesBetweenAddEnergy;
        [SerializeField] private int _energyValueForAdd;

        [SerializeField] private int _currentEnergy;
        private float _timeUntilAddEnergy;

        public int MaxEnergy => _maxEnergy;
        public int CurrentEnergy => _currentEnergy;
        public int StartLevelPrice => _startLevelPrice;
        public int AddForPassingLevel => _addForPassingLevel;
        public int SecondsUntilAddEnergy => (int)_timeUntilAddEnergy - (MinutesUntilAddEnergy * 60) ;
        public int MinutesUntilAddEnergy => (int)(_timeUntilAddEnergy / 60);

        private void Start()
        {
            _timeUntilAddEnergy = _minutesBetweenAddEnergy * 60;
            _currentEnergy = PlayerPrefs.GetInt(CurrentEnergyKey, _maxEnergy);
            
            OnEnergyValueChanged?.Invoke(_currentEnergy, _maxEnergy);
            if (_currentEnergy >= _maxEnergy) return;

            DateTime exitDateTime = DateTime.Parse(PlayerPrefs.GetString(ExitTimeKey));
            TimeSpan exitTimeSpan = DateTime.Now - exitDateTime;

            int addedEnergy = (int)(exitTimeSpan.TotalMinutes / _minutesBetweenAddEnergy);
            addedEnergy *= _energyValueForAdd;

            AddEnergyWithClamp(addedEnergy);
        }

        private void Update()
        {
            if (_timeUntilAddEnergy <= 0)
            {
                _timeUntilAddEnergy = _minutesBetweenAddEnergy * 60;
                AddEnergyWithClamp(_energyValueForAdd);
            }
            _timeUntilAddEnergy -= Time.deltaTime;
        }

        public bool IsEnoughEnergy(int value)
        {
            bool flag = _currentEnergy >= value;

            if (!flag)
            {
                PopUpSystem.Instance.ShowInformationPopUp(_infoTranslateId);
            }
            
            return flag;
        }

        public void AddEnergy(int value)
        {
            _currentEnergy += value;
            OnEnergyValueChanged?.Invoke(_currentEnergy, _maxEnergy);
        }

        public void MinusEnergy(int value)
        {
            _currentEnergy = Mathf.Clamp(_currentEnergy - value, 0, _currentEnergy);
            OnEnergyValueChanged?.Invoke(_currentEnergy, _maxEnergy);
        }

        private void AddEnergyWithClamp(int value)
        {
            _currentEnergy = Mathf.Clamp(_currentEnergy + value, 0, _maxEnergy);
            OnEnergyValueChanged?.Invoke(_currentEnergy, _maxEnergy);
        }

#if !UNITY_EDITOR
        private void OnApplicationPause(bool pauseStatus)
        {
            PlayerPrefs.SetString(ExitTimeKey, DateTime.Now.ToString());
            PlayerPrefs.SetInt(CurrentEnergyKey, _currentEnergy);
        }
#endif
        private void OnApplicationQuit()
        {
            PlayerPrefs.SetString(ExitTimeKey, DateTime.Now.ToString());
            PlayerPrefs.SetInt(CurrentEnergyKey, _currentEnergy);
        }
    }
}