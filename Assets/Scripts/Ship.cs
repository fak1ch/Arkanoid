using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private float _inputValue;

    private void Update()
    {
        _inputValue = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (_inputValue != 0)
        {
            Vector2 newPosition = _rigidbody2D.position;
            newPosition.x += _inputValue * Time.fixedDeltaTime * _speed;
            _rigidbody2D.MovePosition(newPosition);
        }
    }
}
