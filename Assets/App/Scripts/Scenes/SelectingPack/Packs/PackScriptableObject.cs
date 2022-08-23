using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.Scenes.SelectingPack
{
    [CreateAssetMenu(fileName = "New pack", menuName = "Packs")]
    public class PackScriptableObject : ScriptableObject
    {
        public List<PackInformation> packs;

        public PackInformation GetPackById(int id)
        {
            return packs.FirstOrDefault(info => info.Id == id);
        }
    }

    [Serializable]
    public class PackInformation
    {
        public int Id;
        public string Name;
        public Sprite sprite;
        public int levelCount;
    }
}