﻿using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusChangeSpeedPlatform: Bonus
    {
        [SerializeField] private float _duration;
        [SerializeField] private float _addSpeedValue;
        
        protected override void ActivateBonus()
        {
            bonusData.playerController.AddSpeed(_addSpeedValue, _duration);
        }
    }
}