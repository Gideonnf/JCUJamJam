using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player ID to determine which player")]
    [SerializeField]
    [Range(1,2)]
    int playerID; // Player 1 WASD, Player 2 Arrow Key

    [Header("Player Speed")]
    [SerializeField]
    float playerSpeed;

    Vector3 playerInput;
    Rigidbody rgdbdy;
    bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rgdbdy = transform.GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        // playerControls
        PlayerControls();
    }


    private void FixedUpdate()
    {
        rgdbdy.MovePosition(rgdbdy.position + playerInput * playerSpeed * Time.fixedDeltaTime);
    }


    void PlayerControls()
    {
        playerInput = Vector3.zero;
        if (playerID == 1)
        {
            playerInput.x = Input.GetAxis("Horizontal");
            playerInput.z = Input.GetAxis("Vertical");
        }

        if(playerID == 2)
        {
            playerInput.x = Input.GetAxis("Horizontal2");
            playerInput.z = Input.GetAxis("Vertical2");
        }


    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
}
