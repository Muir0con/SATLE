using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// youtube tutorial: https://www.youtube.com/watch?v=K1xZ-rycYY8
// Alternative tutorial for movement, also for animation: https://www.youtube.com/watch?v=v4MVGu1H24s

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    // following for player animation
    float horizontalMove = 0f;
    public float runSpeed = 7f;
    
    // following for player movement controls
    private float horizontal;
    private float speed = 7f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Returns -1, 0, or 1 depending on direction of movement
        horizontal = Input.GetAxisRaw("Horizontal");

        // if jump button pressed while grounded, jump by setting y component of rigid body velocity to jumping power
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // if jump button released and player still moving upwards, multiply verticle velocity by 0.5 - jump higher by holding jump button longer
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

        }

        // Flip player based on movement direction
        Flip();

        // animations
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // Handles jump
    private bool IsGrounded()
    {
        // check if grounded - creates invisible circle at players feet, when collides with groundlayer will allow jump
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    // Flip player - handles movement
    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale; 
        }
    }

}
