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
            
            if (StaticLevelPath.packId != -1)
                currentPack.LevelComplete();

            if (currentPack.CurrentLevelIndex == currentPack.LevelCount)
            {
                SceneManager.LoadScene("SelectingPack");
            }
            else
            {
                StaticLevelPath.levelPath = currentPack.GetLevelPath();
                SceneManager.LoadScene("Level");
            }
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}
