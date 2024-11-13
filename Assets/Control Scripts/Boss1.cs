using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform player; // Reference to the player's position
    public GameObject spearPrefab; // Rust spear projectile
    public float speed = 2f; // Boss follow speed
    public float attackDelay = 2f; // Delay between attacks
    public float spearSpeed = 5f; // Speed of the thrown spear

    private Animator animator; // Reference to the Animator component
    private bool isFollowing = false; // Whether the boss is following the player

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartBossFight()); // Start the fight sequence
    }

    private void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }
    }

    // Coroutine to handle boss fight sequence
    private IEnumerator StartBossFight()
    {
        animator.SetTrigger("FlyUp"); // Trigger fly up animation
        yield return new WaitForSeconds(1f); // Delay to complete fly up animation

        isFollowing = true; // Start following player
        yield return new WaitForSeconds(1f); // Delay before first attack

        for (int i = 0; i < 3; i++) // Boss attacks three times
        {
            animator.SetTrigger("Scanning"); // Trigger scanning animation
            yield return new WaitForSeconds(attackDelay); // Delay for scanning

            animator.SetTrigger("Throwing"); // Trigger throwing animation
            Attack(); // Throw spear at the player
            yield return new WaitForSeconds(attackDelay); // Delay between attacks
        }

        isFollowing = false; // Stop following after attacks (optional)
    }

    // Method to handle following the player horizontally
    private void FollowPlayer()
    {
        Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    // Method to handle spear attack
    private void Attack()
    {
        GameObject spear = Instantiate(spearPrefab, transform.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        spear.GetComponent<Rigidbody2D>().velocity = direction * spearSpeed;
    }
}