using Blocks;
using System;
using Pool;
using UnityEngine;
using UnityEngine.UI;

namespace LevelGeneration
{
    [Serializable]
    public class LevelSpawnerData
    {
        public PoolData<Block> poolData;
        public GridLayoutGroup blockContainer;
        public LevelData levelData;
        public Canvas canvas;
    }
}
