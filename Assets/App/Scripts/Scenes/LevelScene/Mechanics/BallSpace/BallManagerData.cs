using Player;
using Pool;
using System;
using App.Scripts.General.CoroutineManager;
using UnityEngine;
using Walls;

namespace BallSpace
{
    [Serializable]
    public class BallManagerData
    {
        public PoolData<MovableComponent> poolData;
        public PlayerPlatform playerPlatform;
        public BottomWall bottomWall;
        public CoroutineManager coroutineManager;
        public LayerMask ballLayer;
        public LayerMask blockLayer;
        public float startBallSpeed;
        public float maxBallSpeed;
        public float speedJump;
    }
}
