using System.Linq;
using App.Scripts.General.SceneLoaderSpace;
using App.Scripts.Scenes.SelectingPack;
using LevelGeneration;
using UnityEngine;

namespace MainMenuSceneSpace
{
    public class MainMenuUI : MonoBehaviour
    {
        private const string FirstStartKey = "firstStart";
        
        [SerializeField] private PackScriptableObject _packSO;
        private bool _firstStart = true;
        
        private void Start()
        {
            int flag = PlayerPrefs.GetInt(FirstStartKey, 0);
            _firstStart = flag == 0;
        }

        public void PlayButtonEvent()
        {
            if (_firstStart)
            {
                PlayerPrefs.SetInt(FirstStartKey, 1);
                var packInfo = GetPackInfoById(0);
                var packRepository = new PackRepository(packInfo);
                StaticLevelPath.levelPath = packRepository.GetLevelPath();
                StaticLevelPath.packId = packInfo.Id;
                SceneLoader.Instance.LoadSceneById(2);
            }
            else
            {
                SceneLoader.Instance.LoadSceneById(1);
            }
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packSO.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}
