using System;
using Project.Runtime.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using Views;
using TMPro;

namespace Project.Runtime.Scripts.Views
{
    public class GameView : ViewBaseBehaviour
    {
        [SerializeField] private bool timeRunning;
        [SerializeField] private Button pauseButton;
        [SerializeField] private TMP_Text playerPoint;
        [SerializeField] private TMP_Text enemyPoint;
        [SerializeField] private TMP_Text timeText;
        [SerializeField] private Slider levelStat;

        public bool TimeRunning => timeRunning;
        public Button PauseButton => pauseButton;
        public TMP_Text PlayerPoint => playerPoint;
        public TMP_Text EnemyPoint => enemyPoint;
        public TMP_Text TimeText => timeText;
        public Slider LevelStat => levelStat;

        private void Start()
        {
            pauseButton.onClick.AddListener(PauseButtonAction);
            levelStat.value = 0.5f;
            timeRunning = true;
        }

        void PauseButtonAction()
        {
            GameManager.instance.uiManager.pauseView.Open();
        }

        public void LevelStatUpdate(int playerStat, int enemyStat)
        {
            var total = playerStat + enemyStat;
            if (total == 0)
                total = 100;
            levelStat.value = (float)playerStat / total;

            playerPoint.text = playerStat.ToString();
            enemyPoint.text = enemyStat.ToString();
        }

        public float UpdateTimer(float timeData)
        {
            if (timeData > 0)
            {
                timeData -= Time.deltaTime;
            }
            else
            {
                timeRunning = false;
            }

            float minutes = Mathf.FloorToInt(timeData / 60);
            float seconds = Mathf.FloorToInt(timeData % 60);
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            return timeData;
        }
    }
}
