using System;
using System.Collections.Generic;
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
    public class BlockInformation
    {
        public int id;
        public Block block;
        public BlockTypes type;
        public int poolSpawnCount;
    }
}