using System;
using Managers;
using Project.Runtime.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LevelFailedView : ViewBaseBehaviour
    {
        
        [SerializeField] private Button restartButton;
        public Button RestartButton => restartButton;

        private void Start()
        {
            restartButton.onClick.AddListener(RestartButtonAction);
        }

        void RestartButtonAction()
        {
            GameManager.instance.RestartGame();
        }
        public override void Open()
        {
            base.Open();
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
