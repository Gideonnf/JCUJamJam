using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavAgent : MonoBehaviour
{

    enum AI_STATE { IDLE, CHASE, ATTACK, RETURN }
    [SerializeField]
    AI_STATE curAiState;
    GameObject targetPlayer;
    NavMeshAgent agent;
    Vector3 originalPos;

    public Animator AIAnim;
    public bool isDead;


    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        AIAnim.SetBool("isDead", isDead);

        if (isDead)
            return;

        switch (curAiState)
        {
            case AI_STATE.IDLE:
                for(int i = 0; i < 2; i++)
                {
                    if (Vector3.Distance(GameManager.Instance.players[i].transform.position, transform.position) < 12)
                    {
                        SetTarget(GameManager.Instance.players[i]);
                    }
                }

                break;
            case AI_STATE.CHASE:
                agent.stoppingDistance = 5;
                if (Vector3.Distance(targetPlayer.transform.position, transform.position) > 5)
                    agent.SetDestination(targetPlayer.transform.position);
                else
                    curAiState = AI_STATE.ATTACK;
                break;
            case AI_STATE.ATTACK:
                curAiState = AI_STATE.RETURN;

                break;
            case AI_STATE.RETURN: agent.isStopped = false; targetPlayer = null; agent.stoppingDistance = 0; agent.SetDestination(originalPos); break;
        }

        if (curAiState == AI_STATE.RETURN)
        {
            //   Debug.Log(Vector3.Distance(transform.position, originalPos));
            if (Vector3.Distance(transform.position, originalPos) < 2f)
            {
                curAiState = AI_STATE.IDLE;
                agent.isStopped = true;
            }
        }

    }

    public void SetTarget(GameObject target)
    {
        if (target != null && curAiState == AI_STATE.IDLE)
        {


            targetPlayer = target;
            curAiState = AI_STATE.CHASE;
            agent.isStopped = false;



        }
        else if (target == null)
        {
            agent.isStopped = true;
        }

    }

    void ReturnToPatrol()
    {
        curAiState = AI_STATE.RETURN;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && curAiState == AI_STATE.IDLE)
        {
            targetPlayer = other.gameObject;
            curAiState = AI_STATE.CHASE;
            agent.isStopped = false;
            Debug.Log("Fire");
        }
    }
}
