using System;
using System.Collections.Generic;
using Project.Runtime.Scripts.Behaviours;
using UnityEngine;
//using ElephantSDK;

namespace Project.Runtime.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject ground;
        
        public List<LevelBehaviour> levels;

        public LevelBehaviour currentLevel;

        public Action LevelStartAction;
        public Action LevelCompleteAction;
        public Action LevelFailedAction;

        private GameManager gm;

        private void Awake()
        {
            gm = GameManager.instance;
            gm.levelManager = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            LevelStartAction += LevelStart;
            LevelCompleteAction += LevelComplete;
            LevelFailedAction += LevelFailed;
        }

        private void LevelStart()
        {
            LevelStartAction -= LevelStart;
            //Elephant.Core.Elephant.LevelStarted(gm.levelId);
        }

        private void LevelComplete()
        {
            LevelCompleteAction -= LevelComplete;
            //Elephant.Core.Elephant.LevelCompleted(gm.levelId);
            var newLevelId = gm.levelId++;
            PlayerPrefs.SetInt("levelId",newLevelId);
        }

        private void LevelFailed()
        {
            //Elephant.Core.Elephant.LevelFailed(gm.levelId);
            LevelFailedAction -= LevelFailed;
        }

        public void Restart()
        {
            
        }

        public void TransitionNextLevel()
        {
            LevelStartAction += LevelStart;
            LevelCompleteAction += LevelComplete;
            LevelFailedAction += LevelFailed;
            
            Destroy(currentLevel.gameObject);
            var levelScore = currentLevel.levelScore;
            /*var newLevel = Instantiate(levels[GameManager.instance.levelId]);
            currentLevel = newLevel.GetComponent<LevelBehaviour>();*/
            gm.UpdateUI(levelScore, currentLevel.levelName);
            GameManager.instance.StartGame();
        }

        public void MakeGround()
        {
            foreach (var groundP in currentLevel.groundPos)
            {
                Instantiate(this.ground, groundP.position, Quaternion.identity);
            }
        }

    }
}
