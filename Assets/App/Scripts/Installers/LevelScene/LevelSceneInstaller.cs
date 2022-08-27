using System;
using App.Scripts.Scenes.LevelScene.Mechanics.BonusSpace;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses.BonusKinds;
using App.Scripts.Scenes.SelectingPack;
using Architecture;
using BallSpace;
using Blocks.BlockTypesSpace;
using GameEventsControllerSpace;
using InputSystems;
using LevelGeneration;
using ParserJsonSpace;
using Player;
using Pool;
using UnityEngine;

namespace Installers.LevelScene
{
    public class LevelSceneInstaller : Installer
    {
        [SerializeField] private InputSystemInfo _inputSystemSettings;
        [SerializeField] private PlayerControllerInfo _playerSettings;
        [SerializeField] private WallLimitersData _wallLimitersSettings;
        [SerializeField] private LevelSpawnerData _levelSpawnerSettings;
        [SerializeField] private BallManagerData _ballManagerData;
        [SerializeField] private PlayerHealthData _playerHealthData;
        [SerializeField] private BonusSpawnerData _bonusSpawnerData;
        [SerializeField] private BonusesActivatorData _bonusesActivatorData;

        [SerializeField] private DebugLevelData _debugLevelData;

        public override void Install(AppHandler appHandler)
        {
            var inputSystem = new InputSystem(_inputSystemSettings);
            var playerController = new PlayerController(_playerSettings, inputSystem);

            var ballPool = new ObjectPool<MovableComponent>(_ballManagerData.poolData);
            var ballManager = new BallManager(_ballManagerData, ballPool);

            _levelSpawnerSettings.levelData = LoadLevelDataFromJson(StaticLevelPath.levelPath);
            var blockContainer = new BlockContainer(_levelSpawnerSettings.blocksConfig.blocks, _levelSpawnerSettings.blockPoolContainer);
            var wallsLimiters = new WallsLimiters(_wallLimitersSettings);
            var levelSpawner = new LevelSpawner(_levelSpawnerSettings, ballManager, blockContainer);

            var playerHealth = new PlayerHealth(_playerHealthData, ballManager);

            _bonusesActivatorData.playerHealth = playerHealth;
            _bonusesActivatorData.playerController = playerController;
            _bonusesActivatorData.ballManager = ballManager;
            var bonusesActivator = new BonusesActivator(_bonusesActivatorData);
            _bonusSpawnerData.bonusData.bonusesActivator = bonusesActivator;
            var bonusContainer = new BonusContainer(_bonusSpawnerData.bonusList.bonuses, _bonusSpawnerData.bonusContainer);
            var bonusSpawner = new BonusSpawner(_bonusSpawnerData, bonusContainer, levelSpawner);
            var bonusManager = new BonusManager(bonusesActivator);
 
            var gameEventsController = new GameEventsController(playerHealth, ballManager, playerController, levelSpawner, bonusSpawner, 
                bonusManager, inputSystem);
            
            appHandler.AddBehaviour(inputSystem);
            appHandler.AddBehaviour(playerController);
            appHandler.AddBehaviour(ballManager);
            appHandler.AddBehaviour(wallsLimiters);
            appHandler.AddBehaviour(levelSpawner);
            appHandler.AddBehaviour(playerHealth);
            appHandler.AddBehaviour(gameEventsController);
            appHandler.AddBehaviour(bonusSpawner);
            appHandler.AddBehaviour(bonusManager);
        }
        
        private LevelData LoadLevelDataFromJson(string levelDataPath)
        {
            if (StaticLevelPath.packId == -1)
            {
                var packInfo = _debugLevelData.packScriptableObject.GetPackById(_debugLevelData.packId);
                var packRepository = new PackRepository(packInfo);
                levelDataPath = packRepository.GetLevelPathByIndex(_debugLevelData.levelIndex);
            }
            
            var jsonParser = new JsonParser<LevelData>();
            return jsonParser.LoadDataFromFile(levelDataPath);
        }
    }

    [Serializable]
    public class DebugLevelData
    {
        public PackScriptableObject packScriptableObject;
        public int packId = -1;
        public int levelIndex = -1;
    }
}
