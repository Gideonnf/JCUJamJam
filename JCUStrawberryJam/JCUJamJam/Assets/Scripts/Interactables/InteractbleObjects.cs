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

    public virtual void CollisionInteraction(PlayerController playerController, CollisionState colState)
    {

    }

    public virtual void TriggerInteraction(PlayerController playerController, CollisionState colState)
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        // Check if player controller exist in the game object
        if (playerController)
        {
            // If it does check for player 2
            CollisionInteraction(playerController, CollisionState.ENTER);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        // Check if player controller exist in the game object
        if (playerController)
        {
            // If it does check for player 2
            CollisionInteraction(playerController, CollisionState.STAY);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        // Check if player controller exist in the game object
        if (playerController)
        {
            // If it does check for player 2
            CollisionInteraction(playerController, CollisionState.EXIT);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            TriggerInteraction(playerController, CollisionState.ENTER);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            TriggerInteraction(playerController, CollisionState.STAY);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController)
        {
            TriggerInteraction(playerController, CollisionState.EXIT);
        }
    }
}
