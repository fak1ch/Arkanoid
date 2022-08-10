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
        public event Action OnHealthZero;

        private int _maxHealth;
        private int _health;

        public int CurrentHealth => _health;

        public HealthSystem(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                Debug.LogError("You send minus value");
                return;
            }

            if (_health - damage > 0)
            {
                _health -= damage;
            }
            else
            {
                _health = 0;
                OnHealthZero?.Invoke();
            }
        }

        public void AddHealth(int value)
        {
            if (value < 0)
            {
                Debug.LogError("You send minus value");
                return;
            }

            if (_health + value <= _maxHealth)
            {
                _health += value;
            }
            else
            {
                _health = _maxHealth;
            }
        }
    }
}
