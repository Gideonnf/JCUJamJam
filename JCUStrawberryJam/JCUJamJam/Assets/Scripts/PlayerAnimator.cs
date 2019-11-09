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

    }
}
