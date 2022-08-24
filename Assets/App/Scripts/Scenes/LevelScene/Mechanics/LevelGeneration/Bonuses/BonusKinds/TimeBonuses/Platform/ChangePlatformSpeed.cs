using System;
using BallSpace;
using Player;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class ChangePlatformSpeed : TimeBonus
    {
        private float _addSpeedValue;
        private PlayerController _playerController;
        
        public ChangePlatformSpeed(float duration, float addSpeedValue, PlayerController playerController) : base(duration)
        {
            Id = 6;
            _addSpeedValue = addSpeedValue;
            _playerController = playerController;
        }
        
        public override void StartBonus()
        {
            _playerController.AddSpeed(_addSpeedValue);
        }

        protected override void EndBonus()
        {
            _playerController.AddSpeed(_addSpeedValue * -1);
        }
    }
}