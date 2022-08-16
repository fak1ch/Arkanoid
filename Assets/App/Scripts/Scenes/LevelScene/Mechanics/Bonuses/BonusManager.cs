using Architecture;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.BonusSpace
{
    public class BonusManager : CustomBehaviour
    {
        public BonusManager()
        {
            
        }

        public void ActivatePlenaryBall()
        {
            
        }

        public void ActivateAccelerationBall()
        {
            
        }

        public void ActivateDecelerationBall()
        {
            
        }
        
        public void ActivateAccelerationPlatform()
        {
            
        }

        public void ActivateDecelerationPlatform()
        {
            
        }
        
        public void ActivateExtensionPlatform()
        {
            
        }

        public void ActivateCompressPlatform()
        {
            
        }

        public void ActivateBlackTag()
        {
            
        }

        public void ActivateSourceLife()
        {
            
        }

        public void ActivateBallOfFury()
        {
            
        }
        
        private bool CheckEventProbability(float percentProbability)
        {
            return Random.Range(0, 101) >= percentProbability;
        }
    }
}