using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables for movement
    public float speed = 5f;
    public float jumpForce = 10f;
    private bool isJumping = false;
    private Rigidbody2D rb;
    private Animator animator;

    // Variables for push animation
    public bool isTouchingBox;
    // void OnCollisionEnter2D(Collision2D collision);

    // Ground check variables
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    // Ceiling check variables
    public Transform ceilingCheck;
    public float ceilingCheckRadius = 0.2f;
    private bool isTouchingCeiling;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Check if the player is touching the ceiling
        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayer);

        //Check if the player is touching a box
        void OnCollisionEnter2D(Collision2D collision);

        if (collision.gameObject.CompareTag("rust pile"))
            {
                isTouchingBox == true;
            }
        else
            {
                isTouchingBox == false;
            }

        // Handle player input
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Update animation parameters
        animator.SetFloat("speed", Mathf.Abs(moveInput));

        // Handle jumping
        if (!isGrounded && Input.GetButtonDown("Jump") && !isTouchingCeiling)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        // Update jumping animation
        if (isGrounded)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }


    }

   /// void OnCollisionEnter2D(Collision2D collision)
    ///{
        // Example of handling pushing behavior
      ///  if (collision.gameObject.CompareTag("Pushable"))
      //  {
           // Logic to handle pushing objects can be added here
      //      Debug.Log("Pushing an object!");
      //  }
    //}
}