using System;
using Player;
using Pool;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds
{
    public abstract class Bonus : MonoBehaviour
    {
        public event Action<Bonus> OnDestroy;

        [SerializeField] private BonusMovement _movement;
        
        private ObjectPool<Bonus> _pool;
        protected BonusData bonusData;

        private float _bottomCameraY;

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
    }
}