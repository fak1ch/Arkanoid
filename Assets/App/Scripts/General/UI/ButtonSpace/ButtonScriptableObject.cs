using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ButtonSpace
{
    [CreateAssetMenu(fileName = "New Button settings", menuName = "ButtonSettings")]
    public class ButtonScriptableObject : ScriptableObject
    {
        public Color PressedColor = new Color(193, 193, 193);
        public float PressedScaleProcent = 0.5f;
        public float ScaleDuration = 0.5f;
        public float ChangeColorDuration = 0.5f;
    }
}

