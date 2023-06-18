using System;
using System.Collections;
using System.Linq;
using App.Scripts.General.Energy;
using App.Scripts.General.LoadScene;
using App.Scripts.General.LocalizationSystemSpace;
using App.Scripts.Scenes.SelectPack;
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
        [SerializeField] private float _delatUntilContinue;

        [Space(10)] 
        [SerializeField] private Image _galaxyImage;
        [SerializeField] private TextMeshProUGUI _galaxyLevels;
        
        private PackRepository _currentPack;

        private void Awake()
        {
            OnPopUpStartShowAnimation += PopUpStartShowAnimation;
            OnPopUpOpen += PopUpOpen;
        }

        private void PopUpStartShowAnimation(PopUp popUp)
        {
            if (StaticLevelPath.packId == null) return;
            
            var packInfo = GetPackInfoById(StaticLevelPath.packId.Value);
            _currentPack = new PackRepository(packInfo);
            _packNameText.SetId(_currentPack.Name);
            _galaxyLevels.text = $"{_currentPack.CurrentLevelIndex}/{_currentPack.LevelCount}";
            _galaxyImage.sprite = packInfo.sprite;
        }

        private void PopUpOpen(PopUp popUp)
        {
            if (StaticLevelPath.packId == null) return;
            
            _currentPack.LevelComplete();
            _galaxyLevels.text = $"{_currentPack.CurrentLevelIndex}/{_currentPack.LevelCount}";
            EnergySystem.Instance.AddEnergy(EnergySystem.Instance.AddForPassingLevel);
        }

        public void ContinueButtonEvent()
        {
            if (StaticLevelPath.CreateLevelData != null)
            {
                SceneLoader.Instance.LoadScene(SceneEnum.LevelCreator);
                HidePopUp();
                return;
            }

            if (_currentPack.CurrentLevelIndex == _currentPack.LevelCount)
            {
                SceneLoader.Instance.LoadScene(SceneEnum.SelectingPack);
                HidePopUp();
            }
            else
            {
                if (EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice))
                {
                    StartCoroutine(NextLevelRoutine(_currentPack));
                }
            }
        }
        
        private IEnumerator NextLevelRoutine(PackRepository currentPack)
        {
            SetButtonsInteractable(false);
            EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
            yield return new WaitForSeconds(_delatUntilContinue);
            StaticLevelPath.levelPath = currentPack.GetLevelPath();
            SceneLoader.Instance.LoadScene(SceneEnum.Level);
            HidePopUp();
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}