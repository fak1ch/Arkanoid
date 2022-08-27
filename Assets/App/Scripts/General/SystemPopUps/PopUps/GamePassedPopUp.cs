using System;
using System.Collections;
using System.Linq;
using App.Scripts.General.LocalizationSystemSpace;
using App.Scripts.General.SceneLoaderSpace;
using App.Scripts.Scenes.SelectingPack;
using DG.Tweening;
using LevelGeneration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.General.PopUpSystemSpace.PopUps
{
    public class GamePassedPopUp : PopUp
    {
        [SerializeField] private PackScriptableObject _packScriptableObject;
        [SerializeField] private TranslatableText _packNameText;

        [Space(10)] 
        [SerializeField] private Image _galaxyImage;
        [SerializeField] private TextMeshProUGUI _galaxyName;
        [SerializeField] private TextMeshProUGUI _galaxyLevels;

        private void Start()
        {
            if (StaticLevelPath.packId == -1) return;
            var packInfo = GetPackInfoById(StaticLevelPath.packId);
            var currentPack = new PackRepository(packInfo);
            _packNameText.SetId(currentPack.Name);
            _galaxyName.text = currentPack.Name;
            _galaxyLevels.text = $"{currentPack.CurrentLevelIndex + 1}/{currentPack.LevelCount}";
            _galaxyImage.sprite = packInfo.sprite;
        }

        public void ContinueButtonEvent()
        {
            var currentPack = new PackRepository(GetPackInfoById(StaticLevelPath.packId));

            if (StaticLevelPath.packId != -1)
                currentPack.LevelComplete();

            if (currentPack.CurrentLevelIndex == currentPack.LevelCount)
            {
                SceneLoader.Instance.LoadSceneById(1);
            }
            else
            {
                StaticLevelPath.levelPath = currentPack.GetLevelPath();
                SceneLoader.Instance.LoadSceneById(2);
            }
            
            HidePopUp();
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}