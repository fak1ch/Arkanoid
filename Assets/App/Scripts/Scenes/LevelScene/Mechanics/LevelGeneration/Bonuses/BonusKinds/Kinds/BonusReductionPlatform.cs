using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusReductionPlatform: Bonus
    {
        protected override void ActivateBonus()
        {
            Debug.Log("Ball speed+");
        }
    }
}