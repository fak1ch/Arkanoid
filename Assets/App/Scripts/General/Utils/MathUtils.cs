using UnityEngine;

namespace App.Scripts.General.Utils
{
    public class MathUtils
    {
        public static float GetPercent(float a, float b, float value)
        {
            return (value - a) / (b - a);
        }
    }
}