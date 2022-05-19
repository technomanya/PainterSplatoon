using UnityEngine;
using UnityEngine.AI;

namespace Project.Runtime.Scripts.Behaviours
{
    public class EnemyBehaviour : EnemyBaseBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            Invoke(nameof(SetTarget), 1f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(currTarget, transform.position) < 1f)
            {
                paintTargets.RemoveAt(paintTargets.Count-1);
                SetTarget();
            }
        }
    }
}
