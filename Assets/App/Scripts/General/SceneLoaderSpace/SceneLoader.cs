using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.General.Singleton;
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

        private Tween _fadeTween;
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoadedEvent;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoadedEvent;
        }

        private void SceneLoadedEvent(Scene arg0, LoadSceneMode arg1)
        {
            PlayFadeAnimation(1, 0);
            _fadeTween.OnComplete(SetActiveBackgroundFalse);
        }

        private void SetActiveBackgroundTrue()
        {
            _bg.gameObject.SetActive(true);
        }

        private void SetActiveBackgroundFalse()
        {
            _bg.gameObject.SetActive(false);
        }
        
        private void PlayFadeAnimation(float startValue, float endValue)
        {
            var color = _bg.color;
            color.a = startValue;
            _bg.color = color;
            
            _bg.gameObject.SetActive(true);
            _fadeTween.Kill();
            _fadeTween = _bg.DOFade(endValue, _timeUntilLoadScene);
        }
        
        public void LoadSceneById(int id)
        {
            OnSceneStartLoading?.Invoke();
            StartCoroutine(LoadSceneByIdAfterTime(id));
        }
        
        private IEnumerator LoadSceneByIdAfterTime(int id)
        {
            PlayFadeAnimation(0, 1);
            _fadeTween.OnComplete(SetActiveBackgroundTrue);
            yield return new WaitForSeconds(_timeUntilLoadScene);
            SceneManager.LoadScene(GetSceneNameById(id));
        }

        private string GetSceneNameById(int id)
        {
            return _sceneSO.scenes.FirstOrDefault(scene => scene.id == id)?.sceneName;
        }
    }
}