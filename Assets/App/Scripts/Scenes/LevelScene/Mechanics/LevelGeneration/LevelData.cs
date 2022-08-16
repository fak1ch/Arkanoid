using System;
using Blocks.BlockTypesSpace;

namespace LevelGeneration
{
    [Serializable]
    public class LevelData
    {
        public int blocksCountColumn = 5;
        public int blocksCountRow = 5;

        public int[][] blocksIndexesArray;

        public int Size => blocksCountRow * blocksCountColumn;
    }
}
