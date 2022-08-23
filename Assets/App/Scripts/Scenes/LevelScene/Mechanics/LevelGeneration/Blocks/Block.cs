using BallSpace;
using HealthSystemSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.Utils;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using Blocks.BlockTypesSpace;
using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour, IInformation<Block>
    {
        public event Action<Block> OnBlockDestroy;

        [SerializeField] private BlockData _blockData;
        [SerializeField] private BonusScriptableObject _bonusScriptableObject;

        private BlockInformation _blockInformation;
        protected HealthSystem healthSystem;
        protected Block[][] blocks;

        public bool IsDestroyed { get; set; }
        public bool IsImmortality { get; set; }
        public int IndexColumn { get; private set; }
        public int IndexRow { get; private set; }

        public BlockInformation BlockInformation
        {
            get => _blockInformation;
            set => _blockInformation = value;
        }
        public PoolObjectInformation<Block> PoolObjectInformation => BlockInformation;
        public BlockData BlockData => _blockData;

        protected virtual void Start()
        {
            healthSystem = new HealthSystem(0, _blockInformation.maxHealth);
            healthSystem.OnHealthEqualsMinValue += DestroyBlock;
            InitializeBonusImage();
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
            int currentHealth = healthSystem.CurrentHealth;
            int maxHealth = healthSystem.MaxHealth;
            int breakSpritesCount = _blockData.breakSprites.Count - 1;
            float percent = MathUtils.GetPercent(0, maxHealth, currentHealth);
            int newIndex = Mathf.RoundToInt(breakSpritesCount * percent);

            _blockData.blockBreakImage.sprite = _blockData.breakSprites[newIndex];
        }

        private void InitializeBonusImage()
        {
            if (_blockInformation.bonusId == -1) return;

            _blockData.bonusImage.sprite = _bonusScriptableObject.bonuses
                .FirstOrDefault(info => info.id == _blockInformation.bonusId)
                ?.prefab.Sprite;
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

    public interface IInformation<T>
    {
        public PoolObjectInformation<T> PoolObjectInformation { get; }
    }
}
