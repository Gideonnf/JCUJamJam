using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavAgent : MonoBehaviour
{

    enum AI_STATE { IDLE, CHASE, ATTACK,RETURN}
    [SerializeField]
    AI_STATE curAiState;
    GameObject targetPlayer;
    NavMeshAgent agent;
    Vector3 originalPos;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(curAiState)
        {
            case AI_STATE.IDLE: agent.isStopped = true; break;
            case AI_STATE.CHASE:
                agent.isStopped = false;
                agent.stoppingDistance = 5;
                if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 5)
                    agent.SetDestination(targetPlayer.transform.position);
                else
                curAiState = AI_STATE.ATTACK;
                break;
            case AI_STATE.ATTACK:  curAiState = AI_STATE.RETURN;  break;
            case AI_STATE.RETURN:targetPlayer = null; agent.stoppingDistance = 0; agent.SetDestination(originalPos); break;
        }

        if(curAiState == AI_STATE.RETURN)
        {
            Debug.Log(Vector3.Distance(transform.position, originalPos));
            if (Vector3.Distance(transform.position , originalPos) < 2f)
                curAiState = AI_STATE.IDLE;
        }

    }

    public void SetTarget(GameObject target)
    {
        if (target != null && curAiState == AI_STATE.IDLE)
        {
            agent.isStopped = false;

            targetPlayer = target;
            curAiState = AI_STATE.CHASE;
         
            


        }
        else if(target == null)
        {
            agent.isStopped = true;
        }
            
    }

    void ReturnToPatrol()
    {
       
    }
}
