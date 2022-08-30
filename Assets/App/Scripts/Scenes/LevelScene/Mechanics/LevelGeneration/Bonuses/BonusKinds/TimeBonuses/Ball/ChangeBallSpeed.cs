using System;
using BallSpace;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class ChangeBallSpeed : TimeBonus
    {
        private float _addSpeedValue;
        private BallManager _ballManager;
        
        private float _minusValue;
        
        public ChangeBallSpeed(float duration, float addSpeedValue, BallManager ballManager) : base(duration)
        {
            Id = 2;
            _addSpeedValue = addSpeedValue;
            _ballManager = ballManager;
        }
        
        public override void StartBonus()
        {
            _minusValue = _ballManager.AddValueToSpeedAllBalls(_addSpeedValue);
        }

        protected override void EndBonus()
        {
            _ballManager.AddValueToSpeedAllBalls(_minusValue * -1);
        }
    }
}