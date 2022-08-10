using Architecture;
using InputSystems;
using UnityEngine;
using System;
using BallSpace;

namespace Player
{
    public class PlayerHealthView : MonoBehaviour
    {
        public void RefreshHealth(int value)
        {
            Debug.Log("current health: " + value);
        }
    }
}
