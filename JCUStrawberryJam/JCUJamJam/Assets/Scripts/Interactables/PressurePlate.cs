using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractbleObjects
{
    public PlayerState playerNeeded;
    public bool objectNeeded;
    public GameObject LinkedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TriggerInteraction(Collider other, CollisionState colState)
    {
        base.TriggerInteraction(other, colState);
        if(colState == CollisionState.STAY)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if(playerController)
            {
                if (playerController.GetPlayerID() == (int)playerNeeded)
                {
                    // Trigger
                    LinkedObject.GetComponent<Trap>().trapDisabled = true;
                }
            }
            // if the player standing is the same player needed
            else if(objectNeeded)
            {
                InteractbleObjects collidedObject = other.gameObject.GetComponent<InteractbleObjects>();
                if (collidedObject)
                {
                    LinkedObject.GetComponent<Trap>().trapDisabled = true;
                }
            }
        }
        else if (colState == CollisionState.EXIT)
        {
            LinkedObject.GetComponent<Trap>().trapDisabled = false;
        }
    }
}
