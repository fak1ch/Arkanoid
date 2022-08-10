﻿using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace ParserJson
{
    public class JsonParser<T>
    {
        private T _data = default(T);

        public JsonParser() { }

        public void SaveLevelDataToFile(T dataClass, string path)
        {
            _data = dataClass;
            string json = JsonConvert.SerializeObject(dataClass, Formatting.Indented);

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
                UnityWebRequest www = UnityWebRequest.Get(path);
                www.SendWebRequest();

                while (!www.downloadHandler.isDone) { }

                _data = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
            }
            catch (Exception e)
            {
                Debug.LogError("JsonParser loadFromFile: error" + e.Message);
            }

            return _data;
        }
    }
}
