using System.Linq;
using App.Scripts.General.Energy;
using App.Scripts.General.LoadScene;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.SelectPack;
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
            var count = PopUpSystem.Instance.ActivePopUpsCount;
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
                EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
                SceneLoader.Instance.LoadScene(SceneEnum.Level);
            }
            else
            {
                SceneLoader.Instance.LoadScene(SceneEnum.SelectingPack);
            }
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packSO.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}
