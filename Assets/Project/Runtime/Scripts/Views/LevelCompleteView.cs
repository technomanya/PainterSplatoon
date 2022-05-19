using Managers;
using Project.Runtime.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class LevelCompleteView : ViewBaseBehaviour
    {
        [SerializeField] private Button multiplyRewardButton;
        [SerializeField] private Button nextLevelButton;
        
        
        public Button MultiplyRewardButton => multiplyRewardButton;
        public Button NextLevelButton => nextLevelButton;
        
        // Start is called before the first frame update
        void Start()
        {
            nextLevelButton.onClick.AddListener(NextLevel);
            multiplyRewardButton.onClick.AddListener(MultiplyReward);
        }

        void NextLevel()
        {
            GameManager.instance.NextLevel();
        }

        void MultiplyReward()
        {
            GameManager.instance.MultiplyReward();
        }

    }
}
