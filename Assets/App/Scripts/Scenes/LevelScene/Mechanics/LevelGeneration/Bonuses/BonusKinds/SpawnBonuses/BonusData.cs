using System;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses.BonusKinds;
using BallSpace;
using Player;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    [Serializable]
    public class BonusData
    {
        public BonusesActivator bonusesActivator;
        public Camera mainCamera;
        public float cameraBottomYOffset;
    }
}