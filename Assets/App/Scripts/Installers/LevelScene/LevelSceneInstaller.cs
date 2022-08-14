using Architecture;
using BallSpace;
using Blocks;
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
        [SerializeField] private GameEventsControllerData _gameEventsControllerData;

        public override void Install(AppHandler appHandler)
        {
            var inputSystem = new InputSystem(_inputSystemSettings);
            var playerController = new PlayerController(_playerSettings, inputSystem);

            var ballPool = new ObjectPool<MovableComponent>(_ballManagerData.poolData);
            var ballManager = new BallManager(_ballManagerData, ballPool);

            _levelSpawnerSettings.levelData = LoadLevelDataFromJson(StaticLevelPath.LevelPath);
            var wallsLimiters = new WallsLimiters(_wallLimitersSettings);
            var levelSpawner = new LevelSpawner(_levelSpawnerSettings, ballManager);

            var playerHealth = new PlayerHealth(_playerHealthData, ballManager);

            var gameEventsController = new GameEventsController(_gameEventsControllerData, playerHealth, ballManager, playerController, levelSpawner);
 
            appHandler.AddBehaviour(inputSystem);
            appHandler.AddBehaviour(playerController);
            appHandler.AddBehaviour(ballManager);
            appHandler.AddBehaviour(wallsLimiters);
            appHandler.AddBehaviour(levelSpawner);
            appHandler.AddBehaviour(playerHealth);
            appHandler.AddBehaviour(gameEventsController);
        }
        
        private LevelData LoadLevelDataFromJson(string levelDataPath)
        {
            var jsonParser = new JsonParser<LevelData>();
            return jsonParser.LoadLevelDataFromFile(levelDataPath);
        }
    }
}
