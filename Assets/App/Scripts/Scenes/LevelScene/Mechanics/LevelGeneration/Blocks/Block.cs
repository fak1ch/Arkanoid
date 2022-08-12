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
        private int _healthImageIndex;

        private void Start()
        {
            _healthImageIndex = _data.health.Count - 1;
            _healthSystem = new HealthSystem(_data.minHealth, _data.health.Count);
            _data.blockImage.sprite = _data.blockSprite;
            _healthSystem.OnHealthEqualsMinValue += BlockDestroy;
            RefreshDamageSprite();
        }

        private void BlockDestroy()
        {
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
            _data.blockBreakImage.sprite = _data.health[_healthImageIndex].damageSprite;

            if (_healthImageIndex > 0)
                _healthImageIndex--;
        }

        public void RestoreHealth()
        {
            _healthImageIndex = _data.health.Count - 1;
            _healthSystem.RestoreHealth();
            
            RefreshDamageSprite();
        }
    }
}
