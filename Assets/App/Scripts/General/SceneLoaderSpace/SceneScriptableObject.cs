using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.General.SceneLoaderSpace
{
    [CreateAssetMenu(fileName = "New scene", menuName = "Scenes")]
    public class SceneScriptableObject : ScriptableObject
    {
        public List<SceneInformation> scenes;
    }

    [Serializable]
    public partial class SceneInformation
    {
        public int id;
        public string sceneName;
    }
    
#if UNITY_EDITOR
    public partial class SceneInformation : ISerializationCallbackReceiver
    {
        public SceneAsset scene;
        
        public void OnBeforeSerialize()
        {
            if (scene != null)
            {
                sceneName = scene.name;
            }
            else
            {
                sceneName = String.Empty;
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
#endif
}