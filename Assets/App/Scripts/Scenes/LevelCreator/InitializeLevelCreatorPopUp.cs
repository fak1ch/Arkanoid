using App.Scripts.General.PopUpSystemSpace;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.LevelCreatorSpace
{
    public class InitializeLevelCreatorPopUp : PopUp
    {
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private TMP_InputField _columns;
        [SerializeField] private TMP_InputField _rows;

        public void InitializeCallback()
        {
            gameObject.SetActive(false);
            _levelCreator.Initialize(int.Parse(_columns.text), int.Parse(_rows.text));
        }
    }
}