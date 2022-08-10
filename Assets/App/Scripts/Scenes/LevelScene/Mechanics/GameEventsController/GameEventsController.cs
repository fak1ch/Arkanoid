using Architecture;
using Player;
using System;
using UnityEngine;

namespace GameEventsControllerSpace
{
    public class GameEventsControllerData
    {

    }

    public class GameEventsController : CustomBehaviour
    {
        private GameEventsControllerData _data;
        private PlayerHealth _playerHeath;

        public GameEventsController(GameEventsControllerData data, PlayerHealth playerHealth)
        {
            _data = data;
            _playerHeath = playerHealth;
        }

        public override void Initialize()
        {
            _playerHeath.OnHealthEqualsMinusOne += GameOver;
        }

        private void RestartGame()
        {

        }

        private void GameOver()
        {
            Debug.Log("GameOver!!!");
        }
    }
}
