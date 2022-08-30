using App.Scripts.General.LoadScene;
using UnityEngine;

namespace App.Scripts.Scenes.SelectingPack
{
    public class SelectingPackUI : MonoBehaviour
    {
        public void BackButton()
        {
            SceneLoader.Instance.LoadScene(0);
        }
    }
}