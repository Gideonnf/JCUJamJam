using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Animator trapAnim;
    [System.NonSerialized]
    public bool trapDisabled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player Dead");

        // if the trap is disabled, nth gon happen yooo
        if (trapDisabled)
            return;

        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().RespawnPlayer();
        }
        else if (other.tag =="Enemies")
        {
            // do sme shit i think
        }
    }
}
