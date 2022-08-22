using System;
using BallSpace;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class ChangeBallSpeed : TimeBonus
    {
        private float _addSpeedValue;
        private BallManager _ballManager;
        
        public ChangeBallSpeed(float duration, float addSpeedValue, BallManager ballManager) : base(duration)
        {
            Id = 2;
            _addSpeedValue = addSpeedValue;
            _ballManager = ballManager;
        }
        
        public override void StartBonus()
        {
            _ballManager.AddValueToSpeedAllBalls(_addSpeedValue);
        }

        public override void EndBonus()
        {
            _ballManager.AddValueToSpeedAllBalls(_addSpeedValue * -1);
        }
    }
}