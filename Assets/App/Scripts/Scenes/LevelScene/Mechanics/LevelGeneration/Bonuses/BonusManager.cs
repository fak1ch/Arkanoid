using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using Architecture;
using BallSpace;
using LevelGeneration;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.BonusSpace
{
    public class BonusManager : CustomBehaviour
    {
        private BonusManagerData _data;
        private BonusContainer _bonusContainer;
        private LevelSpawner _levelSpawner;
        
        public BonusManager(BonusManagerData bonusManagerData, BonusContainer bonusContainer, 
            LevelSpawner levelSpawner, PlayerHealth playerHealth, BallManager ballManager, PlayerController playerController)
        {
            _data = bonusManagerData;
            _bonusContainer = bonusContainer;
            _levelSpawner = levelSpawner;
            _data.bonusData.playerHealth = playerHealth;
            _data.bonusData.ballManager = ballManager;
            _data.bonusData.playerController = playerController;
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
        }
    }
}