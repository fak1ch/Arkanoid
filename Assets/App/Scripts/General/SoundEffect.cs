using System;
using UnityEngine;

namespace App.Scripts.Scenes.General
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        private void Start()
        {
            _audioSource.clip = _audioClip;
        }

        public void Play()
        {
            _audioSource.Play();
        }
    }
}