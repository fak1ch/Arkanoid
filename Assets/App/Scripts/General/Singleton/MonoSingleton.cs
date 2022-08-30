using System.IO;
using UnityEngine;

namespace App.Scripts.General.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private const string FolderName = "MainSingleton";
        private const string PrefabName = "MainSingleton";
        
        private static T _instance;

        private static bool _applicationIsQuitting = false;
        
        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting) return null;
                
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        if (typeof(T) == typeof(MainSingleton))
                        {
                            string path = Path.Combine(FolderName, PrefabName);
                            var prefab1 = Resources.Load(path);
                            _instance = Instantiate(prefab1 as GameObject).GetComponent<T>();
                        }
                        else
                        {
                            var prefab = MainSingleton.Instance.GetPrefab<T>();
                            _instance = Instantiate(prefab, MainSingleton.Instance.transform);
                        }
                    }
                }
                
                return _instance;
            }

        }
        
        protected virtual void Awake()
        {
            if (_instance == null )
            {
                if (typeof(T) == typeof(MainSingleton))
                    DontDestroyOnLoad(gameObject);
                
                _instance = this as T;
            }
            else
            {
                Destroy ( gameObject );
            }
        }

        public void OnDestroy()
        {
            _applicationIsQuitting = true;
        }
    }
}