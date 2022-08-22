using System;
using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses;
using BallSpace;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses.BonusKinds
{
    public class BonusesActivator
    {
        public event Action<TimeBonus> OnTimeBonusCreated; 
        private BonusesActivatorData _data;
        
        public BonusesActivator(BonusesActivatorData data)
        {
            _data = data;
        }

        public void ActivateChangeBallSpeed(float duration, float addSpeedValue)
        {
            var bonus = new ChangeBallSpeed(duration, addSpeedValue, _data.ballManager);
            StartBonusCreatedEvent(bonus);
        }

        public void ActivatePlatformSpeedBonus(float duration, float addSpeedValue)
        {
            var bonus = new ChangePlatformSpeed(duration, addSpeedValue, _data.playerController);
            StartBonusCreatedEvent(bonus);
        }

        public void ActivateResizePlatformBonus(int sizeIndex, float duration)
        {
            var bonus = new ChangePlatformSize(duration, sizeIndex, _data.playerPlatform);
            StartBonusCreatedEvent(bonus);
        }

        public void ActivateBallOfFuryBonus(float duration)
        {
            var bonus = new BallOfFury(duration, _data.blockLayer, _data.ballLayer, _data.ballManager);
            StartBonusCreatedEvent(bonus);
        }

        public void ActivateCaptiveBallBonus(float duration, Vector2 position, Vector2 direction)
        {
            var bonus = new CaptiveBall(duration, position, direction, _data.ballManager);
            StartBonusCreatedEvent(bonus);
        }
        
        public void ActivateBlackTagBonus(float duration, int healthValue)
        {
            var bonus = new BlackTag(duration, healthValue, _data.playerHealth);
            StartBonusCreatedEvent(bonus);
        }
        
        public void ActivateSourceLifeBonus(float duration, int healthValue)
        {
            var bonus = new SourceLife(duration, healthValue, _data.playerHealth);
            StartBonusCreatedEvent(bonus);
        }
        
        private void StartBonusCreatedEvent(TimeBonus bonus)
        {
            OnTimeBonusCreated?.Invoke(bonus);
        }
    }

    [Serializable]
    public class BonusesActivatorData
    {
        public PlayerHealth playerHealth;
        public PlayerPlatform playerPlatform;
        public PlayerController playerController;
        public BallManager ballManager;
        public LayerMask blockLayer;
        public LayerMask ballLayer;
    }
}