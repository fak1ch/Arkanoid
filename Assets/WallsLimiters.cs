using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsLimiters : MonoBehaviour
{
    [SerializeField] private BoxCollider2D[] _wallVerticalColliders;
    [SerializeField] private BoxCollider2D[] _wallHorizontalColliders;
    [SerializeField] private Canvas _canvas;

    private void Start()
    {
        ResizeVerticalWallColliders();
        ResizeHorizontalWallColliders();
    }

    private void ResizeVerticalWallColliders()
    {
        float height = Screen.height / _canvas.scaleFactor;

        for (int i = 0; i < _wallVerticalColliders.Length; i++)
        {
            Vector2 newSize = _wallVerticalColliders[i].size;
            newSize.y = height;
            _wallVerticalColliders[i].size = newSize;
        }
    }

    private void ResizeHorizontalWallColliders()
    {
        float width = Screen.width / _canvas.scaleFactor;

        for (int i = 0; i < _wallHorizontalColliders.Length; i++)
        {
            Vector2 newSize = _wallHorizontalColliders[i].size;
            newSize.x = width;
            _wallHorizontalColliders[i].size = newSize;
        }
    }
}
