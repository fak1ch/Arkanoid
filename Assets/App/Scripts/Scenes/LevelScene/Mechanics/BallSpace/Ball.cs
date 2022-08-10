using System;
using UnityEngine;

namespace BallSpace
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public int Damage => _damage;
    }
}
