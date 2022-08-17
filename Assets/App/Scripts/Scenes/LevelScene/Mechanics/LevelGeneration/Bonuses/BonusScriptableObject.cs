using System;
using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using Blocks.BlockTypesSpace;
using UnityEngine;

namespace Blocks
{
    [CreateAssetMenu(fileName = "New Bonus", menuName = "Bonuses")]
    public class BonusScriptableObject : ScriptableObject
    {
        public BonusInformation[] bonuses;
    }
    
    [Serializable]
    public class BonusInformation : PoolObjectInformation<Bonus>
    {
        public float spawnChance;
    }
}