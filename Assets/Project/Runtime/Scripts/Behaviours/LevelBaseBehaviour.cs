using System.Collections.Generic;
using UnityEngine;

namespace Project.Runtime.Scripts.Behaviours
{
    public abstract class LevelBaseBehaviour : MonoBehaviour
    {
        public int levelNumber;
        public string levelName;
        public int levelScore;
        public List<Transform> gridPointList;
        public Transform[] groundPos;
        public GameObject coinPrefab, doubleCoinPrefab;
    }
}
