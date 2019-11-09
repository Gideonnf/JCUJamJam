using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator playerAnim;
    PlayerController playerController;

 

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerAnim.SetBool("Pushing", playerController.isPushing);
        playerAnim.SetBool("Running", playerController.isMoving);
        playerAnim.SetBool("Dead", playerController.isDead);

        if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            if(playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0)
            {
                playerController.RespawnPlayer();
            }
        }
    }
}
