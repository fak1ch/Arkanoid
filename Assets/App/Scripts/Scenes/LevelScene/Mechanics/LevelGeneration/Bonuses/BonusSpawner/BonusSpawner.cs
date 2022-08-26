using System.Collections.Generic;
using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using Architecture;
using BallSpace;
using Blocks;
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

        private void TrySpawnBonus(Block block)
        {
            if (block.BlockInformation.bonusId == -1) return;
            var bonus = _bonusContainer.GetObjectFromPoolById(block.BlockInformation.bonusId);

            bonus.SetBonusData(_data.bonusData);
            bonus.transform.position = block.transform.position;
            bonus.rectTransform.sizeDelta = block.BlockData.rectTransform.sizeDelta;
            bonus.SetBoxCollider2DSize(block.BlockData.rectTransform.sizeDelta/2);
            bonus.OnDestroy += DestroyBonus;
            _bonuses.Add(bonus);
            bonus.gameObject.SetActive(true);
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
            bonus.OnDestroy -= DestroyBonus;
            _bonuses.Remove(bonus);
        }

        public void SetBonusesInactive(bool value)
        {
            foreach (var bonus in _bonuses)
            {
                bonus.SetMovableComponentInactive(value);
            }
        }

        public override void Dispose()
        {
            _levelSpawner.OnBlockDestroyed -= TrySpawnBonus;
        }
    }
}