using UnityEngine;

namespace BallSpace
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private MovableComponent _movableComponent;

        public int Damage => _damage;
        public MovableComponent MovableComponent => _movableComponent;
    }
}
