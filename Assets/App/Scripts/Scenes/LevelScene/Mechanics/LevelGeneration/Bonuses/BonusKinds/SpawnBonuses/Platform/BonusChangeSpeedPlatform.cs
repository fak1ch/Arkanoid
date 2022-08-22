using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusChangeSpeedPlatform: Bonus
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _addSpeedValue;
        
        protected override void ActivateBonus()
        {
            bonusData.bonusesActivator.ActivatePlatformSpeedBonus(_duration, _addSpeedValue);
        }
    }
}