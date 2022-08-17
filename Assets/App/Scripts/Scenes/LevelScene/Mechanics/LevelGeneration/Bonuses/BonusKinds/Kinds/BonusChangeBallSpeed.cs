using BallSpace;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusChangeBallSpeed : Bonus
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _addSpeedValue;
        
        protected override void ActivateBonus()
        {
            bonusData.ballManager.ChangeBallsSpeedForTime(_addSpeedValue, _duration);
        }
    }
}