using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Object = System.Object;

namespace ParserJsonSpace
{
    public class JsonParser<T> where T : class, new()
    {
        private T _data;

        public JsonParser()
        {
            _data = new T();
        }

        public void SaveLevelDataToFile(T dataClass, string path)
        {
            _data = dataClass;
            var json = JsonConvert.SerializeObject(dataClass, Formatting.Indented);

            try
            {
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                Debug.LogError("JsonParser + " + e.Message);
            }
        }

        public T LoadLevelDataFromFile(string path)
        {
            try
            {
                var jsonTextFile = Resources.Load<TextAsset>(path);
                
                _data = JsonConvert.DeserializeObject<T>(jsonTextFile.text);
            }
            catch (Exception e)
            {
                Debug.LogError("JsonParser loadFromFile: error" + e.Message);
            }

            return _data;
        }
    }
}
