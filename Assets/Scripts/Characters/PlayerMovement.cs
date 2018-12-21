using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalVelocity = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalVelocity = GetComponent<Rigidbody2D>().velocity.y;

        animator.SetFloat("Horizontal Speed", Mathf.Abs(horizontalMove));
        animator.SetFloat("Vertical Velocity", verticalVelocity);
        
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }

        else if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jump = false;
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }
}