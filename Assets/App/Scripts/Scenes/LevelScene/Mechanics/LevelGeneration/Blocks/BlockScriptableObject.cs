using Blocks.BlockTypesSpace;
using UnityEngine;

namespace Blocks
{
    [CreateAssetMenu(fileName = "New block", menuName = "Blocks")]
    public class BlockScriptableObject : ScriptableObject
    {
        public Block redBlock;
        public Block greenBlock;
        public Block blueBlock;
        public Block yellowBlock;
        public ImmortalBlock immortalBlock;
        public StandartTNT standartTNT;
        public VerticalTNT verticalTNT;
        public HorizontalTNT horizontalTNT;
        public ColorBomb colorBomb;
    }
}