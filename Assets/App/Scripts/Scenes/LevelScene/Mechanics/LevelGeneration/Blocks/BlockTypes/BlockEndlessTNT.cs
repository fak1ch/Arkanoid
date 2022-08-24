using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class BlockEndlessTNT : BlockTNT
    {
        [SerializeField] private float _timeBetweenDestroys;
        
        protected override void Start()
        {
            base.Start();
            _cellSelectable = new CellSelectableEndless(this, blocks);
        }

        protected override void RunAdditionalLogic()
        {
            var blocksForDestroy = _cellSelectable.GetBlocks(_directions);
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