using System;
using Project.Runtime.Scripts.Managers;
using UnityEngine;

namespace Managers
{
    
    public class AudioManager : MonoBehaviour
    {
        public bool IsSFXEnabled
        {
            get
            {
                return _isSFXEnabled;
            }
            set
            {
                _isSFXEnabled = value;
                PlayerPrefs.SetInt("_isSFXEnabled", _isSFXEnabled ? 1 : 0);
            }
        }
        private bool _isSFXEnabled;
        public bool IsMusicEnabled
        {
            get
            {
                return _isMusicEnabled;
            }
            set
            {
                _isMusicEnabled = value;
                PlayerPrefs.SetInt("_isMusicEnabled", _isMusicEnabled ? 1 : 0);
            }
        }
        private bool _isMusicEnabled;
        [HideInInspector] public AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            GameManager.instance.audioManager = this;
            
            _isSFXEnabled = PlayerPrefs.GetInt("_isSFXEnabled", 1) == 1;
            _isMusicEnabled = PlayerPrefs.GetInt("_isMusicEnabled", 1) == 1;
        }
    }
}
