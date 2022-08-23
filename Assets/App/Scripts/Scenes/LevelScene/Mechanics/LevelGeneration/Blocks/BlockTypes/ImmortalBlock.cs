using System;

namespace Blocks.BlockTypesSpace
{
    public class ImmortalBlock : Block
    {
        protected override void Start()
        {
            base.Start();
            IsImmortality = true;
        }
    }
}