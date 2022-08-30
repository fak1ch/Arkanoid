using System.Collections;
using Architecture;
using BallSpace;
using LevelGeneration;
using Player;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using App.Scripts.Scenes.LevelScene.Mechanics.BonusSpace;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses;
using DG.Tweening;
using InputSystems;
using UnityEngine;

namespace GameEventsControllerSpace
{
    public class GameEventsController : CustomBehaviour
    {
        private static GameEventsController _instance;
        public static GameEventsController Instance => _instance;

        private GameEventsControllerData _data;
        private PlayerHealth _playerHeath;
        private BallManager _ballManager;
        private BonusSpawner _bonusSpawner;
        private BonusManager _bonusManager;
        private PlayerController _playerController;
        private LevelSpawner _levelSpawner;
        private InputSystem _inputSystem;

        private bool _gamePassedIsOpen = true;
        private bool _gameOverIsOpen = true;

        public GameEventsController(GameEventsControllerData data , PlayerHealth playerHealth,
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
            _instance = this;
            _playerHeath.OnHealthEqualsMinusOne += GameOver;
            _levelSpawner.OnNoMoreBlocks += GamePassed;
        }

        public void RestartGame()
        {
            _gameOverIsOpen = true;
            _gamePassedIsOpen = true;
            
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
            if (!_gameOverIsOpen) return;
            _gameOverIsOpen = false;
            
            PauseTheGame();
            PopUpSystem.Instance.ShowPopUp<GameOverPopUp>();
        }

        private void GamePassed()
        {
            if (!_gamePassedIsOpen) return;
            
            _data.playerPlatform.StartCoroutine(DelayUntilShowGamePassedPopUp());
            _gamePassedIsOpen = false;
        }
        
        public void PauseTheGame()
        {
            SetupGameInactive(true);
        }

        public void UnpauseTheGame()
        {
            SetupGameInactive(false);
            _gameOverIsOpen = true;
            _gamePassedIsOpen = true;
        }

        private void SetupGameInactive(bool value)
        {
            _playerController.GameOnPause = value;
            _ballManager.SetupBallInactive(value);
            _bonusSpawner.SetBonusesInactive(value);
            _bonusManager.GameOnPause = value;
            _inputSystem.GameOnPause = value;
            _levelSpawner.SetGameOnPauseFlagToBlocks(value);
        }

        private IEnumerator DelayUntilShowGamePassedPopUp()
        {
            yield return new WaitForSeconds(_data.delayUntilShowGamePassedPopUp);
            PauseTheGame();
            PopUpSystem.Instance.ShowPopUp<GamePassedPopUp>();
        }

        public PlayerHealth GetPlayerHealth()
        {
            return _playerHeath;
        }
        
        public override void Dispose()
        {
            _playerHeath.OnHealthEqualsMinusOne -= GameOver;
            _levelSpawner.OnNoMoreBlocks -= GamePassed;
        }
    }
}
