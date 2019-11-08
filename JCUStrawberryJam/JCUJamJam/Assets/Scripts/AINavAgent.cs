using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavAgent : MonoBehaviour
{

    enum AI_STATE { IDLE, CHASE}
    GameObject targetPlayer;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(GameObject target)
    {
        if (target != null)
        {
            agent.isStopped = false;
            targetPlayer = target;
            agent.SetDestination(target.transform.position);
        }
        else if(target == null)
        {
            agent.isStopped = true;
        }
            
    }
}
