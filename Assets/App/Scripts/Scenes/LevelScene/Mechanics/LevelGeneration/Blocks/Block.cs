using BallSpace;
using HealthSystemSpace;
using System;
using System.Linq;
using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        public event Action<Block> OnBlockDestroy;

        [SerializeField] private BlockData _data;

        private HealthSystem _healthSystem;

        private void Start()
        {
            _healthSystem = new HealthSystem(_data.health.Count);
            _data.blockImage.sprite = _data.blockSprite;
            _healthSystem.OnHealthZero += BlockDestroy;
        }

        private void BlockDestroy()
        {
            _healthSystem.OnHealthZero -= BlockDestroy;
            PlayDestroyEffect();
            OnBlockDestroy?.Invoke(this);
        }

        private void PlayDestroyEffect()
        {

        }

        public void SetBoxColliderSize(Vector2 newSize)
        {
            _data.boxCollider.size = newSize;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Ball ball))
            {
                _healthSystem.TakeDamage(ball.Damage);
                RefreshDamageSprite();
            }
        }

        private void RefreshDamageSprite()
        {
            if (_data.health.Count > 1)
                _data.health.RemoveAt(_data.health.Count - 1);

            _data.blockBreakImage.sprite = _data.health.Last().damageSprite;
        }
    }
}
