using System;
using BallSpace;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class ChangePlatformSize : TimeBonus
    {
        private int _sizeIndex;
        private int _defaultSizeIndex;
        private PlayerPlatform _playerPlatform;
        
        public ChangePlatformSize(float duration, int sizeIndex,
            PlayerPlatform playerPlatform) : base(duration)
        {
            Id = 5;
            _sizeIndex = sizeIndex;
            _playerPlatform = playerPlatform;
            _defaultSizeIndex = _playerPlatform.DefaultSizeIndex;
        }
        
        public override void StartBonus()
        {
            _playerPlatform.SetPlatformSize(_sizeIndex);
        }

        protected override void EndBonus()
        {
            _playerPlatform.SetPlatformSize(_defaultSizeIndex);
        }
    }
}