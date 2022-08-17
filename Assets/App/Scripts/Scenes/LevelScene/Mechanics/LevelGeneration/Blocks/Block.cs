using BallSpace;
using HealthSystemSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using Blocks.BlockTypesSpace;
using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        public event Action<Block> OnBlockDestroy;

        [SerializeField] private BlockData _blockData;

        protected HealthSystem healthSystem;
        protected Block[][] blocks;
        private int _healthImageIndex;

        public bool IsDestroyed { get; set; }
        public bool IsImmortality { get; set; }
        public int IndexColumn { get; private set; }
        public int IndexRow { get; private set; }
        public BlockInformation BlockInformation { get; set; }

        protected virtual void Awake()
        {
            _healthImageIndex = _blockData.health.Count - 1;
            healthSystem = new HealthSystem(_blockData.minHealth, _blockData.health.Count);
            healthSystem.OnHealthEqualsMinValue += DestroyBlock;
            _blockData.blockImage.sprite = _blockData.blockSprite;
            RefreshDamageSprite();
        }

        public void DestroyBlock()
        {
            if (IsDestroyed) return;
            
            IsDestroyed = true;
            transform.localScale = new Vector3(0, 0, 0);
            RunAdditionalLogic();
            OnBlockDestroy?.Invoke(this);
        }
        
        public void RestoreBlock()
        {
            IsDestroyed = false;
            _healthImageIndex = _blockData.health.Count - 1;
            healthSystem.RestoreHealth();
            RefreshDamageSprite();
            
            transform.localScale = new Vector3(1, 1, 1);
        }

        protected virtual void RunAdditionalLogic()
        {
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsImmortality) return;
            
            if (collision.gameObject.TryGetComponent(out Ball ball))
            {
                healthSystem.TakeDamage(ball.Damage);
                RefreshDamageSprite();
            }
        }

        private void RefreshDamageSprite()
        {
            _blockData.blockBreakImage.sprite = _blockData.health[_healthImageIndex].damageSprite;

            if (_healthImageIndex > 0)
                _healthImageIndex--;
        }

        public void SetBoxColliderSize(Vector2 newSize)
        {
            _blockData.boxCollider.size = newSize;
        }

        public void SetBlocksMassive(Block[][] blocks1, int indexColumn, int indexRow)
        {
            blocks = blocks1;
            IndexColumn = indexColumn;
            IndexRow = indexRow;
        }
    }
}
