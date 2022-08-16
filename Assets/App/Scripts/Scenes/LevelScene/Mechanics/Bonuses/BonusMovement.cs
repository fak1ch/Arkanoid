using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public class BonusMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        public bool GameOnPause { get; set; }

        private void Update()
        {
            if (!GameOnPause)
                transform.Translate(Vector3.down * (Time.deltaTime * _speed));
        }
    }
}