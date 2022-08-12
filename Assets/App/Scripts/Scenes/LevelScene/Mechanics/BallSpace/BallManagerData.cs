using Player;
using Pool;
using System;
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
        public float startBallSpeed;
        public float maxBallSpeed;
        public float speedJump;
    }
}
