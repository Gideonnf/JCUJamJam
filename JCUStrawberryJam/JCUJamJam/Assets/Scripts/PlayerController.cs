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
    float pMoveSpeed;
    float pRotationSpeed = 1f;

    float playerHorizontal;
    float playerRotInput;
    Rigidbody rgdbdy;
    bool isGrounded = false;
    public bool holdFlare = false;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        rgdbdy = transform.GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // playerControls

        PlayerControls();
    }


    private void FixedUpdate()
    {
        // rgdbdy.MovePosition(rgdbdy.position + playerInput * pMoveSpeed * Time.fixedDeltaTime);
        Vector3 movement = transform.rotation * Vector3.forward;
        if (playerHorizontal > 0)
            rgdbdy.MovePosition(rgdbdy.position + movement * pMoveSpeed * Time.fixedDeltaTime);
        else if(playerHorizontal < 0)
            rgdbdy.MovePosition(rgdbdy.position + -movement * pMoveSpeed * Time.fixedDeltaTime);

        if (playerRotInput != 0)
        {
            Vector3 yRot = new Vector3(0, playerRotInput, 0);
            yRot = yRot.normalized * pRotationSpeed;
            Quaternion deltaRot = Quaternion.Euler(yRot);
            rgdbdy.MoveRotation(rgdbdy.rotation * deltaRot);
        }
        else
        {
            rgdbdy.transform.Rotate(0, 0, 0);
        }
    }


    void PlayerControls()
    {
        //playerInput = Vector3.zero;
        playerRotInput = 0;
        if (playerID == 1)
        {
            //playerInput.x = Input.GetAxis("Horizontal");
            playerRotInput = Input.GetAxis("Horizontal");
            //if (Input.GetKeyDown(KeyCode.A))
            //    transform.Rotate(0, -pRotationSpeed, 0);
           
            playerHorizontal = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.E))
            {
                //if (!holdFlare)
                    gameManager.GetComponent<GameManager>().FlareOn(1);
                holdFlare = true;
            }
               
            if (Input.GetKeyUp(KeyCode.E))
            {
               // if (holdFlare)
                    gameManager.GetComponent<GameManager>().FlareOff(1);
                holdFlare = false;
            }
        }

        if(playerID == 2)
        {
            //playerInput.x = Input.GetAxis("Horizontal2");
            playerRotInput = Input.GetAxis("Horizontal2");
            playerHorizontal = Input.GetAxis("Vertical2");
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
    
                    gameManager.GetComponent<GameManager>().FlareOn(2);
                holdFlare = true;
            }
               
            if (Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                if (holdFlare)
                    gameManager.GetComponent<GameManager>().FlareOff(2);
                holdFlare = false;
            }
              
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
