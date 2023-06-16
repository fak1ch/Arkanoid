using System;
using App.Scripts.General.LoadScene;
using App.Scripts.General.UI.ButtonSpace;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.General
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private ButtonAnimation _button;
        [SerializeField] private SceneEnum _scene;

        private void OnEnable()
        {
            _button.OnClickOccured.AddListener(LoadScene);
        }

        private void OnDisable()
        {
            _button.OnClickOccured.RemoveListener(LoadScene);
        }
        
        private void LoadScene()
        {
            SceneLoader.Instance.LoadScene(_scene);
        }
    }
}