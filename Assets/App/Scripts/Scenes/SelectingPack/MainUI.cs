using App.Scripts.General.SceneLoaderSpace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Scenes.SelectingPack
{
    public class MainUI : MonoBehaviour
    {
        public void BackButton()
        {
            SceneLoader.Instance.LoadSceneById(0);
        }
    }
}