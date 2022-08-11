using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISpace
{
    public class YouWinPanelUI : MonoBehaviour
    {
        public void OpenMenu()
        {
            gameObject.SetActive(true);
        }

        public void ContinueButtonEvent()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
