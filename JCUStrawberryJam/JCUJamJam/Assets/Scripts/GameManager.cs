using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public GameObject[] enemies;
    public GameObject[] players;
    bool[] playersFlareIsOn = new bool[2];

    GameObject currentTarget;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (players == null)
            players = GameObject.FindGameObjectsWithTag("Player");
        if (enemies == null)
            enemies = GameObject.FindGameObjectsWithTag("Enemies");

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FindClosestPlayer()
    {
        GameObject closest = null;
        GameObject otherOne = null;
        float distance = Mathf.Infinity;

        for (int j = 0; j < enemies.Length; j++)
        {
            Vector3 diff = players[0].transform.position - enemies[j].transform.position;
            Vector3 diff2 = players[1].transform.position - enemies[j].transform.position;
            float curDist = diff.sqrMagnitude;
            float curDist2 = diff2.sqrMagnitude;

            if (curDist < distance && playersFlareIsOn[0])
            {
                closest = players[0];
                otherOne = players[1];
                distance = curDist;
            }

            if(curDist2 < distance && playersFlareIsOn[1])
            {
                closest = players[1];
                otherOne = players[0];
                distance = curDist;
            }
            
             enemies[j].GetComponent<AINavAgent>().SetTarget(closest);
           
            
        }
       
        //  return closest;
    }

    public void ResetTarget()
    {
 
        //for(int i =0; i < enemies.Length; i++)
        //{
        //    enemies[i].GetComponent<AINavAgent>().SetTarget(null);
        //}
    }


    public void FlareOn(int index)
    {
        if (index == 1)
            playersFlareIsOn[0] = true;
        else
            playersFlareIsOn[1] = true;

        FindClosestPlayer();
    }

    public void FlareOff(int index)
    {
        if (index == 1)
            playersFlareIsOn[0] = false;
        else
            playersFlareIsOn[1] = false;
        ResetTarget();

    }
}
