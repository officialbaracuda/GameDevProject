using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioController audioController;
    [SerializeField] private GameController gameController;

    [SerializeField] private float runSpeed;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    void Update()
    {
        if (!gameController.IsGameStopped())
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("isJumping", true);
                audioController.PlaySFX(audioController.jump);
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
                animator.SetBool("isCrouching", true);
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void OnCroching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void Jump() {
        jump = true;
        animator.SetBool("isJumping", true);
        audioController.PlaySFX(audioController.jump);
    }
}
