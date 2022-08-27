using System;
using App.Scripts.General.Energy;
using App.Scripts.General.LocalizationSystemSpace;
using App.Scripts.General.SceneLoaderSpace;
using ButtonSpace;
using LevelGeneration;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.Scenes.SelectingPack
{
    public class PackView : MonoBehaviour
    {
        [SerializeField] private PackViewData[] _data;
        [SerializeField] private TranslatableText _translatableText;

        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _packNameText;
        [SerializeField] private TextMeshProUGUI _galaxyText;
        
        [SerializeField] private Image _packImage;
        [SerializeField] private Image _leftImage;
        [SerializeField] private Image _rightImage;
        [SerializeField] private Image _panelImage;
        [SerializeField] private Sprite _packCloseSprite;

        private PackRepository _packRepository;
        private PackInformation _packInformation;
        private int _currentLevelNumber;
        private int _maxLevelNumber;
        private bool _canStartLevel = true;
        private bool _packComplete = false;
        
        public bool PackIsComplete => _packRepository.IsComplete;
        public bool CanOpenNextPack => _packRepository.CanOpenNextPack;
        public bool PackClosed { get; private set; } = false;
        
        public void InitializePack(PackInformation packInformation)
        {
            _packInformation = packInformation;
            _packRepository = new PackRepository(packInformation);
            _translatableText.SetId(_packRepository.Name);

            _currentLevelNumber = _packRepository.CurrentLevelIndex;
            _maxLevelNumber = _packRepository.LevelCount;

            if (PackIsComplete)
            {
                MakePackAsComplete();
            }
            else
            {
                MakePackAsOpenNotComplete();
            }
        }

        public void StartLevel()
        {
            if (!EnergySystem.Instance.IsEnoughEnergy(EnergySystem.Instance.StartLevelPrice)) return;
            if (_canStartLevel == false) return;

            if (_packComplete)
            {
                _packRepository.ClearProgress();
            }

            EnergySystem.Instance.MinusEnergy(EnergySystem.Instance.StartLevelPrice);
            StaticLevelPath.levelPath = _packRepository.GetLevelPath();
            StaticLevelPath.packId = _packInformation.Id;
            SceneLoader.Instance.LoadSceneById(2);
        }
        
        private void ChangePackViewByIndex(int dataIndex)
        {
            var data = _data[dataIndex];
            _packImage.sprite = _packInformation.sprite;
            _leftImage.sprite = data.leftSprite;
            _rightImage.sprite = data.rightSprite;
            _panelImage.sprite = data.panelSprite;
            _levelText.text = $"{_currentLevelNumber}/{_maxLevelNumber}";
            _packNameText.text = _packInformation.Name;
            _galaxyText.color = data.textNameColor;
            _packNameText.color = data.textNameColor;
            _levelText.color = data.textLevelColor;
        }

        public void MakePackAsClosed()
        {
            _translatableText.SetId("NOTFOUND");
            ChangePackViewByIndex(0);
            
            _packImage.sprite = _packCloseSprite;
            _canStartLevel = false;
            PackClosed = true;
        }
        
        public void MakePackAsOpenNotComplete()
        {
            ChangePackViewByIndex(1);
        }
        
        public void MakePackAsComplete()
        {
            ChangePackViewByIndex(2);
            
            if (_currentLevelNumber != _maxLevelNumber) 
                MakePackAsOpenNotComplete();
            
            _packComplete = true;
        }
    }
}