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

    // Ground check variables
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    // Ceiling check variables
    public Transform ceilingCheck;
    public float ceilingCheckRadius = 0.2f;
    private bool isTouchingCeiling;

    public int maxHealth = 12; // Total hits allowed
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Check if the player is touching the ceiling
        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayer);

        // Handle player input
        float moveInput = Input.GetAxis("Horizontal");

        // Move the player
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Update animation parameters
        animator.SetFloat("speed", Mathf.Abs(moveInput));

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
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
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger Works");
        if (other.CompareTag("Spear"))
        {
            TakeDamage(1); // Decrease health by 1 per spear hit
            Destroy(other.gameObject); // Destroy the spear
        }
        else if (other.CompareTag("Ground"))
        {
            // If the spear hits the ground, destroy it
            Destroy(other.gameObject);
        }
    }
     
    

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        
    }

    void Die()
    {
        Debug.Log("Player has died!");
        // Disable player movement and trigger death animation
        animator.SetBool("dead", true);
        yield return new WaitForSeconds(10f);
        animator.SetBool("dead", false);
    }

    
}