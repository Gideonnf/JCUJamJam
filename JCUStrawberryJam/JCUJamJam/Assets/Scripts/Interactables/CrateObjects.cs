using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateObjects : InteractbleObjects
{

    public override void CollisionInteraction(Collision collider, CollisionState colState)
    {

        base.CollisionInteraction(collider, colState);

        PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();

        if (playerController == null)
            return;


        if (Input.GetKey(KeyCode.E))
        {
            if (playerController.GetPlayerID() == 1)
            {
                playerController.pickObject(this.gameObject);
            }
        }
    }

}
