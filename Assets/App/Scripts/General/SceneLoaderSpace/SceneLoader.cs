using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.MonoSingletonSpace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.General.SceneLoaderSpace
{
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public event Action OnSceneStartLoading;
        
        [SerializeField] private float _timeUntilLoadScene;
        [SerializeField] private SceneScriptableObject _sceneSO;
        [SerializeField] private Image _bg;
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoadedAnimation;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoadedAnimation;
        }

        private void SceneLoadedAnimation(Scene arg0, LoadSceneMode arg1)
        {
            _bg.gameObject.SetActive(true);
            _bg.DOFade(1, 0);
            _bg.DOFade(0, _timeUntilLoadScene).OnComplete(SetActiveFalseBg);
        }

        private void SetActiveFalseBg()
        {
            _bg.gameObject.SetActive(false);
        }
        
        public void LoadSceneById(int id)
        {
            OnSceneStartLoading?.Invoke();
            StartCoroutine(LoadSceneByIdAfterTime(id));
        }
        
        private IEnumerator LoadSceneByIdAfterTime(int id)
        {
            _bg.gameObject.SetActive(true);
            _bg.DOFade(0, 0);
            _bg.DOFade(1, _timeUntilLoadScene);
            yield return new WaitForSeconds(_timeUntilLoadScene);
            SceneManager.LoadScene(GetSceneNameById(id));
        }

        private string GetSceneNameById(int id)
        {
            return _sceneSO.scenes.FirstOrDefault(scene => scene.id == id)?.sceneName;
        }
    }
}