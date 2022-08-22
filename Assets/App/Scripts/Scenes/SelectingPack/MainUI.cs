using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.Scenes.SelectingPack
{
    public class MainUI : MonoBehaviour
    {
        public void BackButton()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}