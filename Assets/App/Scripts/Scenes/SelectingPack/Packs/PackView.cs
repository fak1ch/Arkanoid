using System;
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

        public bool PackIsComplete => _packRepository.IsComplete;
        public bool PackClosed { get; private set; } = false;
        
        public void InitializePack(PackInformation packInformation)
        {
            _packInformation = packInformation;
            _packRepository = new PackRepository(packInformation);

            _currentLevelNumber = _packRepository.CurrentLevelIndex;
            _maxLevelNumber = _packRepository.MaxLevelIndex;

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
            if (_canStartLevel == false) return;

            StaticLevelPath.levelPath = _packRepository.GetLevelPath();
            SceneManager.LoadScene("Level");
        }
        
        private void ChangePackViewByIndex(int dataIndex)
        {
            var data = _data[dataIndex];
            _packImage.sprite = _packInformation.sprite;
            _leftImage.sprite = data.leftSprite;
            _rightImage.sprite = data.rightSprite;
            _panelImage.sprite = data.panelSprite;
            _levelText.text = $"{_currentLevelNumber}/{_maxLevelNumber}";
            _packNameText.text = _packInformation.name;
            _galaxyText.color = data.textNameColor;
            _packNameText.color = data.textNameColor;
            _levelText.color = data.textLevelColor;
        }

        public void MakePackAsClosed()
        {
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

            _canStartLevel = false;
        }
    }
}