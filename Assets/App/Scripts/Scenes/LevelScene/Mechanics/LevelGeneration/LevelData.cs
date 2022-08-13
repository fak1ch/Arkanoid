using System;

namespace LevelGeneration
{
    [Serializable]
    public class LevelData
    {
        public int blocksCountRow = 1;
        public int blocksCountColumn = 1;

        public int Size => blocksCountColumn * blocksCountRow;
    }
}
