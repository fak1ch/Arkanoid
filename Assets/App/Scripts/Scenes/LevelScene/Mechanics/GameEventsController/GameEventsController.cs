using Architecture;
using BallSpace;
using LevelGeneration;
using Player;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using App.Scripts.Scenes.LevelScene.Mechanics.BonusSpace;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using InputSystems;

namespace GameEventsControllerSpace
{
    public class GameEventsController : CustomBehaviour
    {
        private static GameEventsController _instance;
        public static GameEventsController Instance => _instance;
        
        private PlayerHealth _playerHeath;
        private BallManager _ballManager;
        private BonusSpawner _bonusSpawner;
        private BonusManager _bonusManager;
        private PlayerController _playerController;
        private LevelSpawner _levelSpawner;
        private InputSystem _inputSystem;

        public GameEventsController(PlayerHealth playerHealth,
            BallManager ballManager, PlayerController playerController, LevelSpawner levelSpawner, 
            BonusSpawner bonusSpawner, BonusManager bonusManager, InputSystem inputSystem)
        {
            _playerHeath = playerHealth;
            _ballManager = ballManager;
            _playerController = playerController;
            _levelSpawner = levelSpawner;
            _bonusSpawner = bonusSpawner;
            _bonusManager = bonusManager;
            _inputSystem = inputSystem;
        }

        public override void Initialize()
        {
            _instance = this;
            _playerHeath.OnHealthEqualsMinusOne += GameOver;
            _levelSpawner.OnNoMoreBlocks += GamePassed;
        }

        public void RestartGame()
        {
            _playerController.RestartPlayerPlatform();
            _playerHeath.RestoreHealth();

            _levelSpawner.RecreateLevel();
            
            _bonusManager.StopAllBonuses();
            _bonusSpawner.DestroyAllBonuses();

            _ballManager.ReturnAllBallsToPool();
            _ballManager.PlaceNewBallToPlayerPlatform();

            UnpauseTheGame();
        }

        private void GameOver()
        {
            PauseTheGame();
            PopUpSystem.Instance.ShowPopUp<GameOverPopUp>();
        }

        private void GamePassed()
        {
            PauseTheGame();
            PopUpSystem.Instance.ShowPopUp<GamePassedPopUp>();
        }
        
        public void PauseTheGame()
        {
            SetupGameInactive(true);
        }

        public void UnpauseTheGame()
        {
            SetupGameInactive(false);
        }

        private void SetupGameInactive(bool value)
        {
            _playerController.GameOnPause = value;
            _ballManager.SetupBallInactive(value);
            _bonusSpawner.SetBonusesInactive(value);
            _bonusManager.GameOnPause = value;
            _inputSystem.GameOnPause = value;
        }

        public override void Dispose()
        {
            _playerHeath.OnHealthEqualsMinusOne -= GameOver;
            _levelSpawner.OnNoMoreBlocks -= GamePassed;
        }
    }
}
