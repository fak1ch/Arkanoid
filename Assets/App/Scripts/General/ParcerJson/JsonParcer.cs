using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace ParcerJson
{
    public class JsonParcer<T>
    {
        private string _savePath;
        private string _fileName = "LevelData.json";

        private T _data = default(T);

        public JsonParcer()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
                _savePath = Path.Combine(Application.persistentDataPath, _fileName);
            #else
                _savePath = Path.Combine(Application.dataPath, _fileName);
            #endif
        }

        public void SaveLevelDataToFile(T dataClass)
        {
            _data = dataClass;
            string json = JsonConvert.SerializeObject(dataClass);

            try
            {
                File.WriteAllText(_savePath, json);
            }
            catch (Exception e)
            {
                Debug.LogError("JsonParcer + " + e.Message);
            }
        }

        public T LoadLevelDataFromFile()
        {
            if (!File.Exists(_savePath))
            {
                Debug.LogError("Json parcer: File not found");
                return _data;
            }

            try
            {
                string json = File.ReadAllText(_savePath);
                _data = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Debug.LogError("JsonParcer loadFromFile: error" + e.Message);
            }

            return _data;
        }
    }
}
