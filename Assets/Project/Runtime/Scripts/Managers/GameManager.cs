using System;
using System.Collections.Generic;
using Managers;
using Project.Runtime.Scripts.Behaviours;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Project.Runtime.Scripts.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public int score;
        public int levelId;
        public bool isGameRunning;

        public int enemyPoints;
        public int playerPoints;


        [HideInInspector] public LevelManager levelManager;
        [HideInInspector] public VibrationManager vibrationManager;
        [HideInInspector] public UIManager uiManager;
        [HideInInspector] public AudioManager audioManager;
        [HideInInspector] public CameraManager cameraManager;

        private void OnEnable()
        {
            uiManager = GameObject.FindGameObjectWithTag("UiManager").GetComponent<UIManager>();
            levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            vibrationManager = GameObject.FindGameObjectWithTag("VibrationManager").GetComponent<VibrationManager>();
        }

        void Start()
        {
            levelId = PlayerPrefs.GetInt("levelId",0);
            score = PlayerPrefs.GetInt("score",0);
            isGameRunning = true;
            StartGame();
        }

        public void StartGame()
        {
            if(levelManager != null && levelManager.levels.Count != 0)
            {
                levelId %= levelManager.levels.Count;
                var level = Instantiate(levelManager.levels[levelId]);
                levelManager.currentLevel = level.GetComponent<LevelBehaviour>();
                levelManager.MakeGround();
                UpdateUI(score, levelManager.currentLevel.levelName);
                levelManager.LevelStartAction.Invoke();
                isGameRunning = true;
            }
            else
            {
                UpdateUI(0,"Level 1");
            }
        }

        private void Update()
        {
            if (isGameRunning) return;
            Debug.Log(levelManager.LevelFailedAction);
            isGameRunning = true;
            
            if(playerPoints >= enemyPoints)
                levelManager.LevelCompleteAction?.Invoke();
            else if(playerPoints < enemyPoints)
                levelManager.LevelFailedAction?.Invoke();
        }

        public void RestartGame()
        {
            if(levelManager && levelManager.currentLevel)
                levelManager.Restart();
            else
            {
                var scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.buildIndex);
            }
        }

        public void UpdateUI(int _score, string level)
        {
            if (_score < 0)
                _score = 0;
            uiManager.scoreText.text = _score.ToString();
            uiManager.levelText.text = level;
        }

        public void NextLevel()
        {
            levelManager.TransitionNextLevel();
        }

        public void MultiplyReward()
        {
            levelManager.currentLevel.levelScore *= 2;
        }
        public void SoundControl(bool mute)
        {
            audioManager.IsSFXEnabled = !mute;
        }
        
        public void VibrationControl(bool vib)
        {
            vibrationManager.IsVibrationEnabled = vib;
            if(vib)
                vibrationManager.PlayHaptic();
        }

    }
}
