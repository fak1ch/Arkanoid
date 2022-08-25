using System;
using System.Linq;
using App.Scripts.Scenes.SelectingPack;
using LevelGeneration;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using MathUtils = App.Scripts.General.Utils.MathUtils;

namespace UISpace
{
    public class VisorPackInfo : MonoBehaviour
    {
        [SerializeField] private PackScriptableObject _packScriptableObject;
        [SerializeField] private Image _packImage;
        [SerializeField] private TextMeshProUGUI _packProcent;
        [SerializeField] private TextMeshProUGUI _levelCount;

        private void Start()
        {
            if (StaticLevelPath.packId == -1) return;
            
            var packInfo = GetPackInfoById(StaticLevelPath.packId);
            var currentPack = new PackRepository(packInfo);
            _levelCount.text = $"{currentPack.CurrentLevelIndex}/{currentPack.LevelCount}";
            var percent = Mathf.RoundToInt(MathUtils.GetPercent(
                0, currentPack.LevelCount, currentPack.CurrentLevelIndex) * 100);
            _packProcent.text = $"{percent}%";
            _packImage.sprite = packInfo.sprite;
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}