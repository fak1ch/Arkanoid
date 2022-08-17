using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusBallOfFury: Bonus
    {
        [SerializeField] private float _duration;
        protected override void ActivateBonus()
        {
            bonusData.ballManager.ActivateBonusBallOfFury(_duration);
        }
    }
}