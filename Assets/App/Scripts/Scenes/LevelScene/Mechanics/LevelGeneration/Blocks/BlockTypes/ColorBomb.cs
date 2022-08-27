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

        private bool _gameOnPause;
        
        protected override void Start()
        {
            base.Start();
            _selectable = new CellSelectableColorBomb(this, blocks);
        }

        protected override void RunAdditionalLogic()
        {
            var blocksForDestroy = _selectable.GetBlocks(_directions);
            StartCoroutine(DestroyBlocks(blocksForDestroy));
        }

        private IEnumerator DestroyBlocks(List<Block> blocksForDestroy)
        {
            foreach (var b in blocksForDestroy)
            {
                b.DestroyBlock();
                yield return new WaitForSeconds(_timeBetweenDestroys);
            }
            
            blocksForDestroy.Clear();
        }
    }
}