using System;
using System.Collections;
using System.Linq;
using App.Scripts.General.Singleton;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Scripts.General.LoadScene
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
        
        public void LoadScene(SceneEnum sceneEnum)
        {
            OnSceneStartLoading?.Invoke();
            StartCoroutine(LoadSceneRoutine(sceneEnum));
        }
        
        private IEnumerator LoadSceneRoutine(SceneEnum sceneEnum)
        {
            PlayFadeAnimation(0, 1);
            _fadeTween.OnComplete(SetActiveBackgroundTrue);
            yield return new WaitForSeconds(_timeUntilLoadScene);
            SceneManager.LoadScene(GetSceneNameByEnum(sceneEnum));
        }

        private string GetSceneNameByEnum(SceneEnum sceneEnum)
        {
            return _sceneSO.scenes.FirstOrDefault(scene => scene.sceneEnumEnum == sceneEnum)?.sceneName;
        }
    }
}