using Architecture;
using BallSpace;
using LevelGeneration;
using Player;
using System;
using App.Scripts.Scenes.LevelScene.Mechanics.BonusSpace;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using InputSystems;
using UISpace;
using UnityEngine;

namespace GameEventsControllerSpace
{
    [Serializable]
    public class GameEventsControllerData
    {
        public MainUI mainUi;
    }

    public class GameEventsController : CustomBehaviour
    {
        private GameEventsControllerData _data;
        private PlayerHealth _playerHeath;
        private BallManager _ballManager;
        private BonusSpawner _bonusSpawner;
        private BonusManager _bonusManager;
        private PlayerController _playerController;
        private LevelSpawner _levelSpawner;
        private InputSystem _inputSystem;

        public GameEventsController(GameEventsControllerData data, PlayerHealth playerHealth,
            BallManager ballManager, PlayerController playerController, LevelSpawner levelSpawner, 
            BonusSpawner bonusSpawner, BonusManager bonusManager, InputSystem inputSystem)
        {
            _data = data;
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
            _playerHeath.OnHealthEqualsMinusOne += GameOver;
            _data.mainUi.OnRestartGame += RestartGame;
            _data.mainUi.OnPauseTheGame += PauseTheGame;
            _data.mainUi.OnUnpauseTheGame += UnpauseTheGame;
            _levelSpawner.OnNoMoreBlocks += GamePassed;
        }

        private void RestartGame()
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
            _data.mainUi.OpenGameOverMenu();
        }

        private void GamePassed()
        {
            PauseTheGame();
            _data.mainUi.OpenYouWinMenu();
        }
        
        private void PauseTheGame()
        {
            SetupGameInactive(true);
        }

        private void UnpauseTheGame()
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
    }
}
