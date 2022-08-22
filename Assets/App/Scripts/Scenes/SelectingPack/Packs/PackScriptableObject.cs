using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SelectingPack
{
    [CreateAssetMenu(fileName = "New pack", menuName = "Packs")]
    public class PackScriptableObject : ScriptableObject
    {
        public List<PackInformation> packs;
    }

    [Serializable]
    public class PackInformation
    {
        public int Id;
        public string Name;
        public Sprite sprite;
    }
}