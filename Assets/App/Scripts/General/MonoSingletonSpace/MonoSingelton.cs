using UnityEngine;

namespace App.Scripts.General.MonoSingletonSpace
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType<T>();
                }
                
                return _instance;
            }

        }

        protected virtual void Start()
        {
            if (_instance == null )
            {
                _instance = this as T;
                DontDestroyOnLoad ( gameObject );
            }
            else
            {
                Destroy ( gameObject );
            }
        }
    }
}