using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Player ID to determine which player")]
    [SerializeField]
    [Range(1, 2)]
    int playerID; // Player 1 WASD, Player 2 Arrow Key

    [Header("Player Speed")]
    [SerializeField]
    float pMoveSpeed;
    [SerializeField]
    float pRotationSpeed = 80f;

    float playerHorizontal;
    float playerRotInput;
    bool isGrounded = false;

    bool holdingObject = false;
    GameObject pickedObject = null;

    public bool holdFlare = false;
    GameManager gameManager;
    Rigidbody rgdbdy;

    Vector3 startingPos;
    Quaternion startingRot;

    [Header("Robot Variables")]
    public float drainSpeed = 0.5f;
    // For Robot
    float speedDebuff;
    float energyLevel;
    [System.NonSerialized]
    public bool isCharging = false;


    [System.NonSerialized]
    public bool isMoving;
    [System.NonSerialized]
    public bool isPushing;
    [System.NonSerialized]
    public bool isDead;
    [System.NonSerialized]
    public bool isPulling;


    // Start is called before the first frame update
    void Start()
    {
        rgdbdy = transform.GetComponent<Rigidbody>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        startingPos = this.transform.position;
        startingRot = this.transform.rotation;
        energyLevel = 100;
    }

    // Update is called once per frame
    void Update()
    {

        // Update player controls
        PlayerControls();
        // Update player stats
        PlayerUpdate();
    }


    private void FixedUpdate()
    {
        Debug.Log("is Pulling : " + isPulling);
        // rgdbdy.MovePosition(rgdbdy.position + playerInput * pMoveSpeed * Time.fixedDeltaTime);
        Vector3 movement = transform.rotation * Vector3.forward;
        if (playerHorizontal > 0)
        {
            isMoving = true;
            rgdbdy.MovePosition(rgdbdy.position + movement * (pMoveSpeed - speedDebuff) * Time.fixedDeltaTime);
            if (holdingObject)
            {
                Debug.Log("is Pulling AHAHAHAH  : " + isPulling);
                isPushing = true;
                isPulling = false;
            }
            else
            {
                Debug.Log("is Pulling BABABAB  : " + isPulling);
                isPushing = false;
               isPulling = false;
            }
        }
        else if (playerHorizontal < 0)
        {
            isMoving = true;
            rgdbdy.MovePosition(rgdbdy.position + -movement * (pMoveSpeed - speedDebuff) * Time.fixedDeltaTime);
            if (holdingObject)
            {
                Debug.Log("is Pulling HERERERE : " + isPulling);
                isPulling = true;
                isPushing = false;
            }
            else
            {
                Debug.Log("is Pulling THERERE : " + isPulling);
                isPushing = false;
                isPulling = false;
            }
        }
        else
        {
            Debug.Log("is Pulling MEHEHEHE : " + isPulling);

            isMoving = false;
           // isPulling = false;
            //isPushing = false;
        }

        if (playerRotInput != 0)
        {
            //Vector3 yRot = new Vector3(0, playerRotInput, 0);
            //yRot = yRot.normalized * pRotationSpeed;
            //Quaternion deltaRot = Quaternion.Euler(yRot);
            //rgdbdy.MoveRotation(rgdbdy.rotation * deltaRot);
            //Debug.Log("player Rot " + playerRotInput);
            //Vector3 rotateVec = new Vector3(playerHorizontal, playerRotInput, 0);
            if (playerRotInput < 0)
            {
                transform.Rotate(Vector3.up * -pRotationSpeed * Time.deltaTime, Space.Self);
            }
            else if (playerRotInput > 0)
            {
                transform.Rotate(Vector3.up * pRotationSpeed * Time.deltaTime, Space.Self);
            }
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

        // If holding an objec
        // the player cant rotate
        if (playerID == (int)PlayerState.ROBOT)
        {
           // Debug.Log("Speed Debuff : " + speedDebuff);
            if (energyLevel <= 0)
                speedDebuff = 6;
            else
                speedDebuff = 0.0f;

            RobotControls();
        }

        if (playerID == (int)PlayerState.HUMAN)
        {
            HumanControls();
        }
    }

    void HumanControls()
    {
        //playerInput.x = Input.GetAxis("Horizontal2");
        playerRotInput = Input.GetAxis("Horizontal2");
        playerHorizontal = Input.GetAxis("Vertical2");
        if (Input.GetKey(KeyCode.KeypadEnter))
        {

            gameManager.GetComponent<GameManager>().FlareOn(1);
            holdFlare = true;
        }

        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (holdFlare)
                gameManager.GetComponent<GameManager>().FlareOff(1);
            holdFlare = false;
        }

    }

    void RobotControls()
    {
        //playerInput.x = Input.GetAxis("Horizontal");
        playerRotInput = Input.GetAxis("Horizontal");
        //if (Input.GetKeyDown(KeyCode.A))
        //    transform.Rotate(0, -pRotationSpeed, 0);

        playerHorizontal = Input.GetAxis("Vertical");
        //if (Input.GetKey(KeyCode.F))
        //{
        //    //if (!holdFlare)
        //    gameManager.GetComponent<GameManager>().FlareOn(1);
        //    holdFlare = true;
        //}

        //if (Input.GetKeyUp(KeyCode.F))
        //{
        //    // if (holdFlare)
        //    gameManager.GetComponent<GameManager>().FlareOff(1);
        //    holdFlare = false;
        //}

        if (holdingObject)
        {
            playerRotInput = 0;

            // Dropping an object
            if (Input.GetKeyUp(KeyCode.E))
            {
                dropObject();
            }

        }
    }

    void PlayerUpdate()
    {
        if (playerID == (int)PlayerState.ROBOT)
        {
            //    Debug.Log("Energy level : " + energyLevel);

            if (!isCharging)
            {
                energyLevel -= drainSpeed * Time.deltaTime;
                if (energyLevel < 0)
                    energyLevel = 0;
            }


        }
        if (playerID == (int)PlayerState.HUMAN)
        {

        }
    }

    public int GetPlayerID()
    {
        return playerID;
    }

    public void RespawnPlayer()
    {
        //isDead = true;
        // Anything else wil go here
        isDead = false;
        isMoving = false;
        isPushing = false;
        this.transform.position = startingPos;
        this.transform.rotation = startingRot;
    }

    public void pickObject(GameObject pickedObject)
    {
        holdingObject = true;
        this.pickedObject = pickedObject;
        // Set the layer so it wont collide with the player
        pickedObject.layer = 10;
        pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        pickedObject.transform.parent = this.transform;

       
    }

    void dropObject()
    {
        // Set the layer back to default
        pickedObject.layer = 0;
        // Turn back kninamtic to false
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        // unparent the object
        pickedObject.transform.parent = null;
        //  Reset Rptatopm
        pickedObject.transform.rotation = pickedObject.GetComponent<CrateObjects>().startingRot;
        //  remove the reference to the game object
        pickedObject = null;
        // No longer holding object
        holdingObject = false;
        isPulling = false;
        isPushing = false;
    }

    public void RechargeBatteries(float rechargeSpeed)
    {
        if (isCharging)
        {
            energyLevel += rechargeSpeed * Time.deltaTime;
            if (energyLevel >= 100)
                energyLevel = 100;
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }
}
