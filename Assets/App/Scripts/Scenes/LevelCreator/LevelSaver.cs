using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using App.Scripts.General.LoadScene;
using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.Scenes.SelectPack;
using Blocks;
using LevelGeneration;
using Newtonsoft.Json;
using ParserJsonSpace;
using TelegramBotEmpta;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.LevelCreatorSpace
{
    public class LevelSaver : MonoBehaviour
    {
        [SerializeField] private PackScriptableObject _packScriptableObject;
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private TelegramBot _telegramBot;
        [SerializeField] private TMP_InputField _packIdInputField;
        [SerializeField] private TMP_InputField _levelIdForReplaceInputField;
        [SerializeField] private List<GameObject> _editorButtons;
        [SerializeField] private List<GameObject> _androidButtons;

        private void Start()
        {
            bool setActiveEditorButtons = true;

            #if !UNITY_EDITOR
                 setActiveEditorButtons = false;
            #endif

            SetActiveEditorButtons(setActiveEditorButtons);
        }

        private LevelData ConvertCurrentMapToLevelData()
        {
            int[][] blocksMap = _levelCreator.GetConvertedGridToArray;

            LevelData levelData = new LevelData
            {
                blocksMap = blocksMap
            };

            return levelData;
        }

        public void AddLevelInPackToEnd()
        {
            var levelData = ConvertCurrentMapToLevelData();

            var packRepository = new PackRepository(GetPackInfoById(Convert.ToInt32(_packIdInputField.text)));
            packRepository.AddLevelInPackToEnd(levelData);
        }

        public void ReplaceLeveInPackByIndex()
        {
            var levelData = ConvertCurrentMapToLevelData();

            var packRepository = new PackRepository(GetPackInfoById(Convert.ToInt32(_packIdInputField.text)));
            packRepository.ReplaceLevelInPack(levelData, Convert.ToInt32(_levelIdForReplaceInputField.text));
        }

        public void SendLevelJsonToDeveloper()
        {
            var levelData = ConvertCurrentMapToLevelData();
            string json = JsonConvert.SerializeObject(levelData, Formatting.Indented);
            byte[] bytes = Encoding.ASCII.GetBytes(json);

            _telegramBot.SendFile(bytes, "level" + _levelIdForReplaceInputField.text + ".json");
        }

        public void TestLevel()
        {
            StaticLevelPath.CreateLevelData = ConvertCurrentMapToLevelData();
            SceneLoader.Instance.LoadScene(SceneEnum.Level);
        }

        public void SetActivePanel(bool value)
        {
            gameObject.SetActive(value);
        }

        private void SetActiveEditorButtons(bool value)
        {
            foreach (var editorButton in _editorButtons)
            {
                editorButton.SetActive(value);
            }
            
            foreach (var androidButton in _androidButtons)
            {
                androidButton.SetActive(!value);
            }
        }
        
        private PackInformation GetPackInfoById(int id)
        {
            return _packScriptableObject.packs.FirstOrDefault(info => info.Id == id);
        }
    }
}