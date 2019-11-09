using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingPad : InteractbleObjects
{
    public GameObject LinkedObject;

    GameObject playerObject;

    public float rechargeSpeed;
    [System.NonSerialized]
    public bool isActivated;

    private void Update()
    {
        // If there is currently a player object standing on this tile
        if(playerObject)
        {
            if(isActivated == false)
            {
                playerObject.GetComponent<PlayerController>().isCharging = false;
            }
        }
    }

    public override void TriggerInteraction(Collider other, CollisionState colState)
    {
        base.TriggerInteraction(other, colState);
        // If it is staying on the collision box
        if (colState == CollisionState.STAY)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController)
            {
                // Check if it is the robot
                if(playerController.GetPlayerID() == (int)PlayerState.ROBOT)
                {
                    if (isActivated)
                    {
                        playerController.isCharging = true;
                        playerObject = other.gameObject;
                        playerController.RechargeBatteries(rechargeSpeed);
                    }
                }
            }
        }
        // It is leaving the charging pad
        else if (colState == CollisionState.EXIT)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController)
            {
                // Check if its the robot
                if(playerController.GetPlayerID() == (int)PlayerState.ROBOT)
                {
                    playerController.isCharging = false;
                    playerObject = null;
                }
            }
        }
    }

}
