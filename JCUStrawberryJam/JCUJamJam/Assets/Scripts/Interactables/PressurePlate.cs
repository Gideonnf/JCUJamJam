using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractbleObjects
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TriggerInteraction(PlayerController playerController, CollisionState colState)
    {
        base.TriggerInteraction(playerController, colState);
        if(colState == CollisionState.STAY)
        {

        }
    }
}
