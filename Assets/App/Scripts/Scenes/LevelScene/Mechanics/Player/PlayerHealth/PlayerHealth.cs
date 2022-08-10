using Architecture;
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
        }

        private void TakeDamage()
        {
            _healthSystem.TakeDamage(_data.damageForPassBall);

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
    }
}
