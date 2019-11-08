using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractbleObjects : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        // Check if player controller exist in the game object
        if (playerController)
        {
            // If it does check for player 2
            if(Input.GetKey(KeyCode.E))
            {
                if (playerController.GetPlayerID() == 1)
                {
                    playerController.pickObject(this.gameObject);
                }
            }
        }
    }
}
