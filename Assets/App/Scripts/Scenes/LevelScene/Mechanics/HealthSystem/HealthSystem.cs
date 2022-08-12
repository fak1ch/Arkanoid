using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HealthSystemSpace
{
    public class HealthSystem
    {
        public event Action OnHealthEqualsMinValue;

        private int _minValue;
        private int _maxValue;
        private int _health;

        public int CurrentHealth => _health;
        public int MaxHealth => _maxValue;

        public HealthSystem(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _health = _maxValue;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError("You send minus value");
                return;
            }

            if (_health - damage > _minValue)
            {
                _health -= damage;
            }
            else
            {
                _health = _minValue;
                OnHealthEqualsMinValue?.Invoke();
            }
        }

        public void AddHealth(int value)
        {
            if (value < 0)
            {
                Debug.LogError("You send minus value");
                return;
            }

            if (_health + value <= _maxValue)
            {
                _health += value;
            }
            else
            {
                _health = _maxValue;
            }
        }

        public void RestoreHealth()
        {
            _health = _maxValue;
        }
    }
}
