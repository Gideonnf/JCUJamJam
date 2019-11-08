using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateObjects : InteractbleObjects
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void CollisionInteraction(PlayerController playerController, InteractbleObjects.CollisionState colState)
    {
        base.CollisionInteraction(playerController, colState);
        if (Input.GetKey(KeyCode.E))
        {
            if (playerController.GetPlayerID() == 1)
            {
                playerController.pickObject(this.gameObject);
            }
        }
    }

}
