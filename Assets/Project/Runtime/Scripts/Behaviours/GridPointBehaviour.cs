using System;
using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Scripts.Managers;
using UnityEngine;

namespace Project.Runtime.Scripts.Behaviours
{
    public enum PointColourState
    {
        Neutral,
        Player,
        Enemy,
        
    }
    public class GridPointBehaviour : MonoBehaviour
    {
        public PointColourState currPointColour;
        public PointColourState prevPointColour;

        private Material _mat;

        private BoxCollider _boxCollider;
        // Start is called before the first frame update
        void Start()
        {
            currPointColour = prevPointColour = PointColourState.Neutral;
            _mat = GetComponent<MeshRenderer>().material;
            _boxCollider = GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Enum.GetName(typeof(PointColourState), currPointColour)))
            {
                return;
            }

            /*if (other.CompareTag("Player"))
            {
                currPointColour = PointColourState.Player;
                _mat.color = Color.blue;
            }*/
            else if(other.CompareTag("Enemy"))
            {
                if(other.GetType() == typeof(SphereCollider))
                    other.GetComponent<EnemyBaseBehaviour>().paintTargets.Add(this);
                /*else
                {
                    currPointColour = PointColourState.Enemy;
                    other.GetComponent<EnemyBaseBehaviour>().paintTargets.Remove(this);
                    _mat.color = Color.red;
                }*/
                
            }
        }


        public void PaintAndTag(PointColourState colourState)
        {
            SetColliderState(_boxCollider,false);
            var coroutine = EnableCollider();
            StartCoroutine(coroutine);
            if(currPointColour == colourState)
                return;

            switch (currPointColour)
            {
                case PointColourState.Player:
                    GameManager.instance.playerPoints--;
                    break;
                case PointColourState.Enemy:
                    GameManager.instance.enemyPoints--;
                    break;
            }
            currPointColour = colourState;
            switch (currPointColour)
            {
                case PointColourState.Player:
                    _mat.color = Color.blue;
                    GameManager.instance.playerPoints++;
                    break;
                case PointColourState.Enemy:
                    _mat.color = Color.red;
                    GameManager.instance.enemyPoints++;
                    break;
                default:
                    _mat.color = Color.gray;
                    break;
            }
        }

        void SetColliderState(Collider col, bool state)
        {
            col.enabled = state;
        }

        IEnumerator EnableCollider()
        {
            yield return new WaitForSeconds(1f);
            SetColliderState(_boxCollider,true);
        }
    }
}
