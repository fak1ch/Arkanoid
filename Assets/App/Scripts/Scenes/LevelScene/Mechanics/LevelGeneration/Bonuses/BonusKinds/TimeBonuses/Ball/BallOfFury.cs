using System;
using BallSpace;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class BallOfFury : TimeBonus
    {
        private int _blockLayerId;
        private int _ballLayerId;
        private BallManager _ballManager;
        
        public BallOfFury(float duration, LayerMask blockLayer, LayerMask ballLayer, BallManager ballManager) : base(duration)
        {
            Id = 0;
            _blockLayerId = (int)Mathf.Log(blockLayer.value, 2);
            _ballLayerId = (int)Mathf.Log(ballLayer.value, 2);
            _ballManager = ballManager;
        }
        
        public override void StartBonus()
        {
            Physics2D.IgnoreLayerCollision(_blockLayerId, _ballLayerId, true);
            _ballManager.SetToAllBallsBallFuryFlag(true);
        }

        protected override void EndBonus()
        {
            Physics2D.IgnoreLayerCollision(_blockLayerId, _ballLayerId, false);
            _ballManager.SetToAllBallsBallFuryFlag(false);
        }
    }
}