using Architecture;
using UnityEngine;

namespace LevelGeneration
{
    public class WallsLimiters : CustomBehaviour
    {
        private WallLimitersData _settings;

        public WallsLimiters(WallLimitersData settings)
        {
            _settings = settings;
        }

        public override void Initialize()
        {
            ResizeWallColliders();
        }

        private void ResizeWallColliders()
        {
            Vector2 cameraSize = new Vector2(Screen.width / _settings.canvas.scaleFactor, Screen.height / _settings.canvas.scaleFactor);

            for (int i = 0; i < _settings.wallsData.Length; i++)
            {
                float width = (cameraSize.x * _settings.wallsData[i].scaleFactor.x) + 1;
                float height = (cameraSize.y * _settings.wallsData[i].scaleFactor.y) + 1;
                Vector2 newSize = new Vector2(width, height);
                _settings.wallsData[i].collider.size = newSize;
            }
        }
    }
}
