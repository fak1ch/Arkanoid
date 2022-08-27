using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Scripts.General.Singleton
{
    public class MainSingleton : MonoSingleton<MainSingleton>
    {
        [SerializeField] private List<MonoBehaviour> _prefabs = new List<MonoBehaviour>();

        public T GetPrefab<T>() where T : MonoBehaviour
        {
            var prefab = _prefabs.FirstOrDefault(
                x => x.GetType() == typeof(T));

            return prefab as T;
        }
    }
}