using Blocks;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class Bonus : MonoBehaviour
    {
        [SerializeField] private BonusMovement _bonusMovement;

        public bool GameOnPause
        {
            get => _bonusMovement.GameOnPause;
            set => _bonusMovement.GameOnPause = value;
        }

        public virtual void ActivateBonus()
        {
            
        }
    }
}