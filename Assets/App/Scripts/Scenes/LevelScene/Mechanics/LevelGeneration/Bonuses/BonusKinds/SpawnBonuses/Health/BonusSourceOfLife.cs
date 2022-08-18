using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusSourceOfLife: Bonus
    {
        [SerializeField] private float _duration;
        [SerializeField] private int _addHealthValue;
        
        protected override void ActivateBonus()
        {
            bonusData.bonusesActivator.ActivateSourceLifeBonus(_duration, _addHealthValue); 
        }
    }
}