using System;
using App.Scripts.General.PopUpSystemSpace;
using LevelGeneration;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.LevelCreatorSpace
{
    public class InitializeLevelCreatorPopUp : PopUp
    {
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private TMP_InputField _columns;
        [SerializeField] private TMP_InputField _rows;

        private void Start()
        {
            TryBackLastLevel();
        }

        private void TryBackLastLevel()
        {
            if (StaticLevelPath.CreateLevelData == null) return;
            
            InitializeMap(StaticLevelPath.CreateLevelData.BlocksCountColumn, 
                StaticLevelPath.CreateLevelData.BlocksCountRow);
            _levelCreator.LoadLevelData(StaticLevelPath.CreateLevelData);

            StaticLevelPath.CreateLevelData = null;
        }

        public void InitializeButtonClickedCallback()
        {
            InitializeMap(int.Parse(_columns.text), int.Parse(_rows.text));
        }

        private void InitializeMap(int columns, int rows)
        {
            gameObject.SetActive(false);
            _levelCreator.Initialize(columns, rows);
        }
    }
}