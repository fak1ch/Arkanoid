namespace Blocks.BlockTypesSpace
{
    public enum BlockTypes
    {
        Red,
        Green,
        Blue,
        Yellow,
        ImmortalBlock,
        StandartTNT,
        VerticalTNT,
        HorizontalTNT,
        ColorBomb,
    };

    public class BlockContainer
    {
        private BlockScriptableObject _blockList;
        
        public BlockContainer(BlockScriptableObject blockList)
        {
            _blockList = blockList;
        }

        public Block GetBlockByEnum(BlockTypes blockTypes)
        {
            Block block = null;
            
            switch (blockTypes)
            {
                case BlockTypes.Red:
                    block = _blockList.redBlock;
                    break;
                case BlockTypes.Green:
                    block = _blockList.greenBlock;
                    break;
                case BlockTypes.Blue:
                    block = _blockList.blueBlock;
                    break;
                case BlockTypes.Yellow:
                    block = _blockList.yellowBlock;
                    break;
                case BlockTypes.ImmortalBlock:
                    block = _blockList.immortalBlock;
                    break;
                case BlockTypes.StandartTNT:
                    block = _blockList.standartTNT;
                    break;
                case BlockTypes.HorizontalTNT:
                    block = _blockList.horizontalTNT;
                    break;
                case BlockTypes.VerticalTNT:
                    block = _blockList.verticalTNT;
                    break;
                case BlockTypes.ColorBomb:
                    block = _blockList.colorBomb;
                    break;
            }

            return block;
        }
    }
}