using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.General.Utils
{
    public class MapMathUtils
    {
        public static Vector2 CalculateCellSize(Canvas canvas, GridLayoutGroup grid, int columnCount)
        {
            float newBlockWidth = Screen.width / canvas.scaleFactor;
            newBlockWidth -= grid.padding.left + grid.padding.right;
            newBlockWidth -= grid.spacing.x * (columnCount - 1);
            newBlockWidth /= columnCount;

            float percent = MathUtils.GetPercent(0, grid.cellSize.x, newBlockWidth);

            return new Vector2(newBlockWidth, grid.cellSize.y * percent);
        }
    }
}