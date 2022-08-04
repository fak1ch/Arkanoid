using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsLimiters : MonoBehaviour
{
    [SerializeField] private BoxCollider2D[] _wallVerticalColliders;
    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
        ResizeVerticalWallColliders();
    }

    private void ResizeVerticalWallColliders()
    {
        int cameraHeight = _mainCamera.pixelHeight;

        for (int i = 0; i < _wallVerticalColliders.Length; i++)
        {
            Vector2 newSize = _wallVerticalColliders[i].size;
            newSize.y = cameraHeight;
            _wallVerticalColliders[i].size = newSize;
        }
    }
}
