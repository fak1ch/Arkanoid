using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TelegramBotEmpta
{
    public class TelegramBot : MonoBehaviour
    {
        private const string Token = "6249429905:AAEyt5psYv4l7BJ8xeLcOB3BejoPdxGotxQ";
        private readonly string ChatId = "802216622";

        public void SendFile(byte[] bytes, string filename, string caption = "")
        {
            string url = $"https://api.telegram.org/bot{Token}/";
            
            WWWForm form = new WWWForm();
            form.AddField("chat_id", ChatId);
            form.AddField("caption", caption);
            form.AddBinaryData("document", bytes, filename, "filename");
            UnityWebRequest www = UnityWebRequest.Post(url + "sendDocument?", form);
            StartCoroutine(SendRequest(www));
        }
        
        private IEnumerator SendRequest(UnityWebRequest www)
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                var w = www;
                Debug.Log("Success!\n" + www.downloadHandler.text);
            }
        }
    }
}
