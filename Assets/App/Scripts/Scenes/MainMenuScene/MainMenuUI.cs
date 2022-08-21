using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenuSceneSpace
{
    public class MainMenuUI : MonoBehaviour
    {
        public void PlayButtonEvent()
        {
            SceneManager.LoadScene("SelectingPack");
        }
    }
}
