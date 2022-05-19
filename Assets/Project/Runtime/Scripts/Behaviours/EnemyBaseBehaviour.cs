using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Runtime.Scripts.Behaviours
{
    public abstract class EnemyBaseBehaviour : MonoBehaviour
    {
        public NavMeshAgent navAgent;
        
        public List<GridPointBehaviour> paintTargets;
        public Vector3 currTarget;

        public virtual void SetTarget()
        {
            currTarget = paintTargets[paintTargets.Count-1].transform.position;
            navAgent.destination = currTarget;
            /*paintTargets.ForEach(target =>
            {
                if(target.currPointColour == PointColourState.Player || target.currPointColour == PointColourState.Neutral)
                {
                    navAgent.destination = target.transform.position;
                    paintTargets.Remove(target);
                }
            });*/
        }
    }
}
