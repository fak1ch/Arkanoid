using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusBlackTag: Bonus
    {
        [SerializeField] private float _duration;
        [SerializeField] private int _minusHealthValue;
        
        protected override void ActivateBonus()
        {
            bonusData.bonusesActivator.ActivateBlackTagBonus(_duration, _minusHealthValue);
        }
    }
}