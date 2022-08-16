using System.Collections.Generic;
using UnityEngine;

namespace Blocks.BlockTypesSpace
{
    public class CellSelectableEndless : CellSelectable
    {
        public CellSelectableEndless(Block block, Block[][] blocks) : base(block, blocks)
        {
            
        }
        
        public override List<Block> GetBlocks(Vector2[] directions)
        {
            int k = 1;
            
            while (true)
            {
                int neighborsCount = blocksForDestroy.Count;
                foreach (var dir in directions)
                {
                    MakeStepFromCurrentPosition(dir * k);
                }
                
                if (neighborsCount == blocksForDestroy.Count) break;

                k++;
            }

            return blocksForDestroy;
        }
    }
}