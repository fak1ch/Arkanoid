using Blocks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelGeneration
{
    [Serializable]
    public class LevelSpawnerData
    {
        public Block blockPrefab;
        public GridLayoutGroup blockContainer;
        public LevelData levelData;
        public Canvas canvas;
        public GameObjectManipulator manipulator;
    }
}
