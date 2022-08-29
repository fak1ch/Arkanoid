using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class ColorBomb : Block
    {
        [SerializeField] private Vector2[] _directions;
        [SerializeField] private float _timeBetweenDestroys;
        private CellSelectableColorBomb _selectable;

        private List<Block> _blocksForDestroy = new List<Block>();
        private bool _gameOnPause;
        private float _timeUntilDestroy;
        
        protected override void Start()
        {
            base.Start();
            _selectable = new CellSelectableColorBomb(this, blocks);
        }

        private void Update()
        {
            if (_blocksForDestroy.Count == 0) return;

            _timeUntilDestroy -= Time.deltaTime;
            if (_timeUntilDestroy < 0)
            {
                _timeUntilDestroy = _timeBetweenDestroys;
                _blocksForDestroy[0].DestroyBlock();
                _blocksForDestroy.RemoveAt(0);
            }
        }

        public override void RestoreBlock()
        {
            base.RestoreBlock();
            _blocksForDestroy.Clear();
        }

        protected override void RunAdditionalLogic()
        {
            _blocksForDestroy = _selectable.GetBlocks(_directions);
        }
    }
}