using System.Collections.Generic;
using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using Architecture;
using BallSpace;
using LevelGeneration;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.BonusSpace
{
    public class BonusSpawner : CustomBehaviour
    {
        private BonusSpawnerData _data;
        private BonusContainer _bonusContainer;
        private LevelSpawner _levelSpawner;

        private List<Bonus> _bonuses;

        public BonusSpawner(BonusSpawnerData bonusSpawnerData, BonusContainer bonusContainer, 
            LevelSpawner levelSpawner)
        {
            _bonuses = new List<Bonus>();
            _data = bonusSpawnerData;
            _bonusContainer = bonusContainer;
            _levelSpawner = levelSpawner;
        }

        public override void Initialize()
        {
            _levelSpawner.OnBlockDestroyed += TrySpawnBonus;
        }

        private void TrySpawnBonus(Vector2 position)
        {
            var bonus = _bonusContainer.GetRandomBonus();
            
            if (bonus == null) return;

            bonus.SetBonusData(_data.bonusData);
            bonus.gameObject.SetActive(true);
            bonus.transform.position = position;
            bonus.OnDestroy += DestroyBonus;
            _bonuses.Add(bonus);
        }

        public void DestroyAllBonuses()
        {
            while (_bonuses.Count > 0)
            {
                _bonuses[0].ReturnToPool();
            }
        }
        
        private void DestroyBonus(Bonus bonus)
        {
            _bonuses.Remove(bonus);
        }

        public void SetBonusesInactive(bool value)
        {
            foreach (var bonus in _bonuses)
            {
                bonus.SetMovableComponentInactive(value);
            }
        }
    }
}