using System;
using Blocks;
using Blocks.BlockTypesSpace;

namespace LevelGeneration
{
    [Serializable]
    public class LevelData
    {
        public int blocksCountColumn = 5;
        public int blocksCountRow = 5;

        public BlockTypes[,] blockTypes;

        public int Size => blocksCountRow * blocksCountColumn;
    }
}
