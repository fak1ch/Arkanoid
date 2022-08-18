using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusResizePlatform: Bonus
    {
        [SerializeField] private int _platformSizeIndex;
        [SerializeField] private float _duration;
        
        protected override void ActivateBonus()
        {
            bonusData.playerPlatform.SetPlatformSize(_platformSizeIndex, _duration);
        }
    }
}