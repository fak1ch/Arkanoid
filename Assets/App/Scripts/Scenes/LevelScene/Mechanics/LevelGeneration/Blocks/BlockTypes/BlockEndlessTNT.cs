namespace Blocks.BlockTypesSpace
{
    public class BlockEndlessTNT : BlockTNT
    {
        protected override void Start()
        {
            _cellSelectable = new CellSelectableEndless(this, blocks);
        }
    }
}