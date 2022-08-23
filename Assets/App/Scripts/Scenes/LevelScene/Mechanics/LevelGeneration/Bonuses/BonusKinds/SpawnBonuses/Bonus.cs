using System;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using Blocks;
using Player;
using Pool;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public abstract class Bonus : MonoBehaviour, IInformation<Bonus>
    {
        public event Action<Bonus> OnDestroy;
        public RectTransform rectTransform;

        [SerializeField] private BonusMovement _movement;
        [SerializeField] private Image _bonusImage;
        [SerializeField] private BoxCollider2D _boxCollider2D;

        private ObjectPool<Bonus> _pool;
        protected BonusData bonusData;
        private float _bottomCameraY;
        
        public BonusInformation BonusInformation { get; set; }
        public Sprite Sprite => _bonusImage.sprite;
        public PoolObjectInformation<Bonus> PoolObjectInformation => BonusInformation;

        protected abstract void ActivateBonus();

        protected virtual void Start()
        {
            _bottomCameraY = bonusData.mainCamera.ViewportToWorldPoint(new Vector2(0, 0)).y;
            _bottomCameraY -= bonusData.cameraBottomYOffset;
        }

        private void Update()
        {
            CheckBonusOutOfBottomBound();
        }

        private void CheckBonusOutOfBottomBound()
        {
            if (transform.position.y < _bottomCameraY)
            {
                ReturnToPool();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (collider2d.TryGetComponent(out PlayerPlatform platform))
            {
                ActivateBonus();
                ReturnToPool();
            }
        }

        public void SetPool(ObjectPool<Bonus> pool)
        {
            _pool = pool;
        }

        public void ReturnToPool()
        {
            _pool.ReturnElementToPool(this);
            OnDestroy?.Invoke(this);
            gameObject.SetActive(false);
        }

        public void SetBonusData(BonusData bonusData1)
        {
            bonusData = bonusData1;
        }

        public void SetMovableComponentInactive(bool value)
        {
            _movement.enabled = !value;
        }

        public void SetBoxCollider2DSize(Vector2 size)
        {
            _boxCollider2D.size = size;
        }
    }
}