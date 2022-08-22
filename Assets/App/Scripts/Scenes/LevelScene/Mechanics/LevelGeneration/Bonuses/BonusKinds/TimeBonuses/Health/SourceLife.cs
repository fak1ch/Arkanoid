using System;
using BallSpace;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class SourceLife : TimeBonus
    {
        private int _healthValue;
        private PlayerHealth _playerHealth;
        
        public SourceLife(float duration, int healthValue, PlayerHealth playerHealth) : base(duration)
        {
            Id = 4;
            _healthValue = healthValue;
            _playerHealth = playerHealth;
        }
        
        public override void StartBonus()
        {
            _playerHealth.AddHealth(_healthValue);
        }

        public override void EndBonus()
        {
            
        }
    }
}