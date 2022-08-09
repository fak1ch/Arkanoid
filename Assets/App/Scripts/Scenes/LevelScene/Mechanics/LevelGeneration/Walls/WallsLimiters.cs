using Architecture;
using UnityEngine;

namespace LevelGeneration
{
    public class WallsLimiters : CustomBehaviour
    {
        private WallLimitersData _wallLimitersData;

        public WallsLimiters(WallLimitersData settings)
        {
            _wallLimitersData = settings;
        }

        public override void Initialize()
        {
            ResizeWallColliders();
        }

        private void ResizeWallColliders()
        {
            Vector2 cameraSize = new Vector2(Screen.width / _wallLimitersData.canvas.scaleFactor, Screen.height / _wallLimitersData.canvas.scaleFactor);

            for (int i = 0; i < _wallLimitersData.wallsData.Length; i++)
            {
                float width = (cameraSize.x * _wallLimitersData.wallsData[i].scaleFactor.x) + 1;
                float height = (cameraSize.y * _wallLimitersData.wallsData[i].scaleFactor.y) + 1;
                Vector2 newSize = new Vector2(width, height);
                _wallLimitersData.wallsData[i].collider.size = newSize;
            }
        }
    }
}
