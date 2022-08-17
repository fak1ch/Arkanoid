using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusCaptiveBall: Bonus
    {
        [SerializeField] private Vector2[] _directions;
        
        protected override void Start()
        {
            base.Start();
            ActivateBonus();
            ReturnToPool();
        }

        protected override void ActivateBonus()
        {
            bonusData.ballManager.SpawnBallAtPositionWithDirection(transform.position, GetRandomDirection());
        }

        private Vector2 GetRandomDirection()
        {
            return _directions[Random.Range(0, _directions.Length)];
        }
    }
}