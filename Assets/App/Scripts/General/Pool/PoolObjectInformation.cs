using System;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer
{
    [Serializable]
    public class PoolObjectInformation<T>
    {
        public int id;
        public T prefab;
        public int poolSize;
    }
}