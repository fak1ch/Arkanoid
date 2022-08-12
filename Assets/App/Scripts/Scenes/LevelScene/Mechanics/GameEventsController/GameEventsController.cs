﻿using Architecture;
using BallSpace;
using LevelGeneration;
using Player;
using System;
using UISpace;
using UnityEngine;

namespace GameEventsControllerSpace
{
    [Serializable]
    public class GameEventsControllerData
    {
        public MainUI mainUi;
        public PlayerPlatform playerPlatform;
    }

    public class GameEventsController : CustomBehaviour
    {
        private GameEventsControllerData _data;
        private PlayerHealth _playerHeath;
        private BallManager _ballManager;
        private PlayerController _playerController;
        private LevelSpawner _levelSpawner;

        public GameEventsController(GameEventsControllerData data, PlayerHealth playerHealth,
            BallManager ballManager, PlayerController playerController, LevelSpawner levelSpawner)
        {
            _data = data;
            _playerHeath = playerHealth;
            _ballManager = ballManager;
            _playerController = playerController;
            _levelSpawner = levelSpawner;
        }

        public override void Initialize()
        {
            _playerHeath.OnHealthEqualsMinusOne += GameOver;
            _data.mainUi.OnRestartGame += RestartGame;
            _data.mainUi.OnPauseTheGame += PauseTheGame;
            _data.mainUi.OnUnpauseTheGame += UnpauseTheGame;
        }

        private void RestartGame()
        {
            _data.playerPlatform.RestartPlayerPlatform();
            _playerHeath.RestoreHealth();

            _levelSpawner.RecreateLevel();

            _ballManager.ReturnAllBallsToPool();
            _ballManager.PlaceNewBallToPlayerPlatform();
        }

        private void GameOver()
        {
            _data.mainUi.OpenGameOverMenu();
        }

        private void PauseTheGame()
        {
            _playerController.GameOnPause = true;
            _ballManager.StopAllBalls();
        }

        private void UnpauseTheGame()
        {
            _playerController.GameOnPause = false;
            _ballManager.BallsContinueMove();
        }
    }
}
