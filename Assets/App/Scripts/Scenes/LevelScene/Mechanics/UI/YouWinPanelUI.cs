using System;
using System.Linq;
using App.Scripts.Scenes.SelectingPack;
using LevelGeneration;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISpace
{
    public class YouWinPanelUI : MonoBehaviour
    {
        [SerializeField] private PackScriptableObject _packScriptableObject;
        
        public void OpenMenu()
        {
            gameObject.SetActive(true);
        }

        public void ContinueButtonEvent()
        {
            var currentPack = new PackRepository(GetPackInfoById(StaticLevelPath.packId));
            currentPack.LevelComplete();
            SceneManager.LoadScene("MainMenu");
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.id == id);
        }
    }
}
