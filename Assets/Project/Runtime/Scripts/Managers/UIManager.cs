using System;
using Project.Runtime.Scripts.Managers;
using Project.Runtime.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Views;
using Button = UnityEngine.UI.Button;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public GameView gameView;
        public LevelCompleteView levelCompleteView;
        public LevelFailedView levelFailedView;
        public PauseView pauseView;
        public Text levelText;
        public Text scoreText;
        [SerializeField] private Button startButton;
        public Button StartButton => startButton;

        private void Awake()
        {
            GameManager.instance.uiManager = this;
        }

        void Start()
        {
            startButton.onClick.AddListener(StartButtonAction);
        }

        void StartButtonAction()
        {
            gameView.Open();
            GameManager.instance.StartGame();
        }

        private void Update()
        {
            gameView.LevelStatUpdate(GameManager.instance.playerPoints,GameManager.instance.enemyPoints);
            if(gameView.TimeRunning)
            {
                var timeData = GameManager.instance.levelManager.currentLevel.currLevelTime;
                GameManager.instance.levelManager.currentLevel.currLevelTime = gameView.UpdateTimer(timeData);
            }
            else
            {
                GameManager.instance.isGameRunning = false;
            }
        }
    }
}
