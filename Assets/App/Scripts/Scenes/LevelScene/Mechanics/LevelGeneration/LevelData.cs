using System;

namespace LevelGeneration
{
    [Serializable]
    public class LevelData
    {
        public int blocksCountRow = 4;
        public int blocksCountColumn = 4;

        public int Size => blocksCountColumn * blocksCountRow;
    }
}
