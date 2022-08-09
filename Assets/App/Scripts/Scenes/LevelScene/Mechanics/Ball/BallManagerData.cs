using Player;
using Pool;
using System;
using UnityEngine;

namespace Ball
{
    [Serializable]
    public class BallManagerData
    {
        public PoolData poolData;
        public MovableComponent ballPrefab;
        public PlayerPlatform playerPlatform;
        public float startBallSpeed;
        public float maxBallSpeed;
        public float speedJump;
    }
}
