﻿using Architecture;
using System;
using BallSpace;
using HealthSystemSpace;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerHealthData
    {
        public int minHealth;
        public int maxHealth;
        public int damageForPassBall;
        public PlayerHealthView healthView;
    }

    public class PlayerHealth : CustomBehaviour
    {
        public event Action OnHealthEqualsMinusOne;

        private PlayerHealthData _data;
        private HealthSystem _healthSystem;
        private BallManager _ballManager;

        public int CurrentHealth => _healthSystem.CurrentHealth;
        public int MinHealth => _healthSystem.MinHealth;

        public PlayerHealth(PlayerHealthData playerHealthData, BallManager ballManager)
        {
            _data = playerHealthData;
            _ballManager = ballManager;
            _healthSystem = new HealthSystem(_data.minHealth, _data.maxHealth);
        }

        public override void Initialize()
        {
            _ballManager.OnCurrentBallsZero += TakeDamage;
            _healthSystem.OnHealthEqualsMinValue += HealthEqualsMinValue;
            _data.healthView.InitializeHealthView(_healthSystem.MaxHealth);
        }

        private void TakeDamage()
        {
            _healthSystem.TakeDamage(_data.damageForPassBall);
            RefreshPlayerHealthView();
        }

        public void AddHealth(int value)
        {
            _healthSystem.AddHealth(value);
            RefreshPlayerHealthView();
        }

        public void MinusHealth(int value)
        {
            _healthSystem.TakeDamage(value);
            RefreshPlayerHealthView();
        }

        private void RefreshPlayerHealthView()
        {
            _data.healthView.RefreshHealth(_healthSystem.CurrentHealth);
        }

        private void HealthEqualsMinValue()
        {
            OnHealthEqualsMinusOne?.Invoke();
        }

        public void RestoreHealth()
        {
            _healthSystem.RestoreHealth();
            RefreshPlayerHealthView();
        }
    }
}
