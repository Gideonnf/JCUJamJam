using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : InteractbleObjects
{
    public GameObject endText;
    public override void TriggerInteraction(Collider other, CollisionState colState)
    {
        base.TriggerInteraction(other, colState);
        if(colState == CollisionState.ENTER)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController)
            {
                endText.SetActive(true);
                playerController.gameEnd = true;
            }
        }
    }
}
