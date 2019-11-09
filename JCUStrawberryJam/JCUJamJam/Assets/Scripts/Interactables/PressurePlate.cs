using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractbleObjects
{
    public PlayerState playerNeeded;
    public bool objectNeeded;
    public GameObject LinkedObject;

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
                    if(LinkedObject.GetComponent<Trap>() != null)
                    {
                        LinkedObject.GetComponent<Trap>().trapDisabled = true;
                    }
                    else if (LinkedObject.GetComponent<ChargingPad>() != null)
                    {
                        LinkedObject.GetComponent<ChargingPad>().isActivated = true;
                    }
                    if(LinkedObject.GetComponent<Door>() != null)
                    {
                        LinkedObject.GetComponent<Door>().isOpened = true;
                    }
                }
            }
            // if the player standing is the same player needed
            else if(objectNeeded)
            {
                InteractbleObjects collidedObject = other.gameObject.GetComponent<InteractbleObjects>();
                if (collidedObject)
                {
                    if(LinkedObject)
                        LinkedObject.GetComponent<Trap>().trapDisabled = true;
                }
            }
        }
        else if (colState == CollisionState.EXIT)
        {
            // Trigger
            if (LinkedObject.GetComponent<Trap>() != null)
            {
                LinkedObject.GetComponent<Trap>().trapDisabled = false;
            }
            else if (LinkedObject.GetComponent<ChargingPad>() != null)
            {
                LinkedObject.GetComponent<ChargingPad>().isActivated = false;
            }
            else if (LinkedObject.GetComponent<Door>() != null)
            {
                LinkedObject.GetComponent<Door>().isOpened = false;
            }
        }
    }
}
