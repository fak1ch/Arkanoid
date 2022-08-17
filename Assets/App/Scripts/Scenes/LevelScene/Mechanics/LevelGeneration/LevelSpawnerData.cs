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
        public GridLayoutGroup blockContainer;
        public Transform blockPoolContainer;
        public LevelData levelData;
        public BlockScriptableObject blocksConfig;
        public Canvas canvas;
    }
}
