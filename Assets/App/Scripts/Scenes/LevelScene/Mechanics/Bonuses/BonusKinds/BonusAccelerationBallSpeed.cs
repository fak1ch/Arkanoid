using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public class BonusAccelerationBallSpeed : Bonus
    {
        public override void ActivateBonus()
        {
            Debug.Log("+Speed to bonus");
        }
    }
}