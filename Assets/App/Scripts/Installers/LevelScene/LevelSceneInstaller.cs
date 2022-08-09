using Architecture;
using Ball;
using InputSystems;
using LevelGeneration;
using Player;
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

        public override void Install(AppHandler appHandler)
        {
            var inputSystem = new InputSystem(_inputSystemSettings);
            var playerController = new PlayerController(_playerSettings, inputSystem);

            var wallsLimiters = new WallsLimiters(_wallLimitersSettings);
            var levelSpawner = new LevelSpawner(_levelSpawnerSettings);

            var ballManager = new BallManager(_ballManagerData); 

            appHandler.AddBehaviour(inputSystem);
            appHandler.AddBehaviour(playerController);
            appHandler.AddBehaviour(wallsLimiters);
            appHandler.AddBehaviour(levelSpawner);
            appHandler.AddBehaviour(ballManager);
        }
    }
}
