using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusBlackTag: Bonus
    {
        [SerializeField] private int _minusHealthValue;
        
        protected override void ActivateBonus()
        {
            bonusData.playerHealth.MinusHealth(_minusHealthValue);
        }
    }
}