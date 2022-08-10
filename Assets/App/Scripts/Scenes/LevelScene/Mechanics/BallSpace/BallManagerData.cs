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
        public PoolData poolData;
        public MovableComponent ballPrefab;
        public PlayerPlatform playerPlatform;
        public BottomWall bottomWall;
        public float startBallSpeed;
        public float maxBallSpeed;
        public float speedJump;
    }
}
