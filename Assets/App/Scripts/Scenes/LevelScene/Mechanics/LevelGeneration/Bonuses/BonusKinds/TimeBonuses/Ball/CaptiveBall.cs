using System;
using BallSpace;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class CaptiveBall : TimeBonus
    {
        private Vector2 _position;
        private Vector2 _direction;
        private BallManager _ballManager;
        
        public CaptiveBall(float duration, Vector2 position, Vector2 direction,
            BallManager ballManager) : base(duration)
        {
            Id = 1;
            _position = position;
            _direction = direction;
            _ballManager = ballManager;
        }
        
        public override void StartBonus()
        {
            
        }

        public override void EndBonus()
        {
            _ballManager.SpawnBallAtPositionWithDirection(_position, _direction);
        }
    }
}