using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace ParserJson
{
    public class JsonParser<T>
    {
        private string _savePath;
        private string _fileName = "LevelData.json";

        private T _data = default(T);

        public JsonParser()
        {
            _savePath = Path.Combine(Application.persistentDataPath, _fileName);
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
                Debug.LogError("JsonParser + " + e.Message);
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
                Debug.LogError("JsonParser loadFromFile: error" + e.Message);
            }

            return _data;
        }
    }
}
