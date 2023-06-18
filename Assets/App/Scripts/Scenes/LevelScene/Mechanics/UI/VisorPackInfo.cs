using System;
using System.Linq;
using App.Scripts.Scenes.SelectPack;
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
        [SerializeField] private TextMeshProUGUI _packPercent;
        [SerializeField] private TextMeshProUGUI _levelCount;

        private void Start()
        {
            if (StaticLevelPath.packId == null) return;
            
            var packInfo = GetPackInfoById(StaticLevelPath.packId.Value);
            var currentPack = new PackRepository(packInfo);
            _levelCount.text = $"{currentPack.CurrentLevelIndex}/{currentPack.LevelCount}";
            var percent = Mathf.RoundToInt(MathUtils.GetPercent(
                0, currentPack.LevelCount, currentPack.CurrentLevelIndex) * 100);

            _packPercent.text = $"{percent}%";
            _packImage.sprite = packInfo.sprite;
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}