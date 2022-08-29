using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class BlockEndlessTNT : BlockTNT
    {
        [SerializeField] private float _timeBetweenDestroys;
        [SerializeField] private float _timeUntilDestroy;
        
        private List<Block> _blocksForDestroy = new List<Block>();
        
        protected override void Start()
        {
            base.Start();
            _cellSelectable = new CellSelectableEndless(this, blocks);
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
            _blocksForDestroy = _cellSelectable.GetBlocks(_directions);
        }
    }
}