using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusCaptiveBall: Bonus
    {
        [SerializeField] private Vector2[] _directions;

        private void OnEnable()
        {
            if (bonusData == null) return;
            
            ActivateBonus();
            ReturnToPool();
        }

        protected override void ActivateBonus()
        {
            bonusData.bonusesActivator.ActivateCaptiveBallBonus(0, transform.position, GetRandomDirection());
        }

        private Vector2 GetRandomDirection()
        {
            return _directions[Random.Range(0, _directions.Length)];
        }
    }
}