using System;
using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses;
using Blocks;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses
{
    [Serializable]
    public class BonusManagerData
    {
        public BonusData bonusData;
        public BonusScriptableObject bonusList;
        public Transform bonusContainer;
    }
}