using System;
using Blocks;
using Blocks.BlockTypesSpace;

namespace LevelGeneration
{
    [Serializable]
    public class LevelData
    {
        public int BlocksCountColumn => blocksMap.Length;
        public int BlocksCountRow => blocksMap[0].Length;

        public BlockJsonData[][] blocksMap;

        public int Size => BlocksCountRow * BlocksCountColumn;
    }
}
