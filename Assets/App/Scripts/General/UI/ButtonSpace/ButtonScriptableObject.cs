using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButtonSpace
{
    [CreateAssetMenu(fileName = "New Button settings", menuName = "ButtonSettings")]
    public class ButtonScriptableObject : ScriptableObject
    {
        public Color pressedColor;
        public float changeColorDuration;
        public float pressedScalePercent;
        public float scaleDuration;
    }
}

