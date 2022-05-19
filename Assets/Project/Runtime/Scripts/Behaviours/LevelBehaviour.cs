using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Runtime.Scripts.Behaviours
{
    public class LevelBehaviour : LevelBaseBehaviour
    {
        public float levelTime;
        public float currLevelTime;

        public List<GameObject> coins;

        private void Start()
        {
            coins = new List<GameObject>();
            currLevelTime = levelTime;
        }

        private void Update()
        {
            if (Mathf.FloorToInt(currLevelTime) % 5 == 0 && coins.Count == 0)
            {
                var randIndex = Random.Range(0, gridPointList.Count);
                var coin = Instantiate(coinPrefab);
                coin.transform.position = gridPointList[randIndex].position + Vector3.up;
                coin.transform.rotation = Quaternion.identity;
                coins.Add(coin);
            }
        }
    }
}
