using System;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using Blocks.BlockTypesSpace;
using UnityEngine;

namespace Blocks
{
    [CreateAssetMenu(fileName = "New block", menuName = "Blocks")]
    public class BlockScriptableObject : ScriptableObject
    {
        public BlockInformation[] blocks;
    }
    
    [Serializable]
    public class BlockInformation : PoolObjectInformation<Block>
    {
        public BlockTypes type;
        public BlockColors color;
        public int bonusId;
        public int maxHealth;
    }
}