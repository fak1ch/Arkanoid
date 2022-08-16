using System;
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
                float maxValueSize = Math.Max(cameraSize.x, cameraSize.y);
                Vector2 newSize = new Vector2(maxValueSize, maxValueSize);

                var offsetFactor = _wallLimitersData.wallsData[i].offsetFactor;
                var scaleFactor = _wallLimitersData.wallsData[i].scaleFactor;
                var collider = _wallLimitersData.wallsData[i].collider;
                
                collider.size = (newSize * scaleFactor) + Vector2.one;
                collider.offset = new Vector2(newSize.x, newSize.y) * offsetFactor;
            }
        }
    }
}
