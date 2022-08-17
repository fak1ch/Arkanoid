using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusSourceOfLife: Bonus
    {
        [SerializeField] private int _addHealthValue;
        
        protected override void ActivateBonus()
        {
            bonusData.playerHealth.AddHealth(_addHealthValue);
        }
    }
}