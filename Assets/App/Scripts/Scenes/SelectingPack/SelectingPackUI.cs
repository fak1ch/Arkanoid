using System;
using App.Scripts.General.SceneLoaderSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Scenes.SelectingPack
{
    public class SelectingPackUI : MonoBehaviour
    {
        public void BackButton()
        {
            SceneLoader.Instance.LoadSceneById(0);
        }
    }
}