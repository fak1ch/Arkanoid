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

        public bool SaveDataToFile(T dataClass, string path)
        {
            FileInfo file = new FileInfo(path);
            file.Directory.Create();

            _data = dataClass;
            var json = JsonConvert.SerializeObject(dataClass, Formatting.Indented);
            
            try
            {
                File.WriteAllText(path, json);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("JsonParser + " + e.Message);
            }

            return false;
        }

        public T LoadDataFromFile(string path)
        {
            try
            {
                var jsonTextFile = Resources.Load<TextAsset>(path);
                
                _data = JsonConvert.DeserializeObject<T>(jsonTextFile.text);
            }
            catch (Exception e)
            {
                Debug.LogWarning("File not exist, returned default data" + e.Message);
            }

            return _data;
        }
    }
}
