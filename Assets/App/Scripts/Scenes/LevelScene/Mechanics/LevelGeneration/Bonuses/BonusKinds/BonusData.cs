using System;
using BallSpace;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    [Serializable]
    public class BonusData
    {
        public PlayerHealth playerHealth;
        public PlayerPlatform playerPlatform;
        public PlayerController playerController;
        public BallManager ballManager;
        public Camera mainCamera;
        public float cameraBottomYOffset;
    }
}