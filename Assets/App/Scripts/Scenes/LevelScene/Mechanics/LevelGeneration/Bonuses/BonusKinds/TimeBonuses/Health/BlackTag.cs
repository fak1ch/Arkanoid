using System;
using BallSpace;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class BlackTag : TimeBonus
    {
        private int _healthValue;
        private PlayerHealth _playerHealth;
        
        public BlackTag(float duration, int healthValue, PlayerHealth playerHealth) : base(duration)
        {
            Id = 3;
            _healthValue = healthValue;
            _playerHealth = playerHealth;
        }
        
        public override void StartBonus()
        {
            if (_playerHealth.CurrentHealth == _playerHealth.MinHealth + 1) return;
            
            _playerHealth.MinusHealth(_healthValue);
        }

        public override void EndBonus()
        {
            
        }
    }
}