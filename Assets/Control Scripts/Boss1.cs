using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform player; 
    public GameObject spearPrefab; 
    public float speed = 2f; 
    public float attackDelay = 2f;
    public float spearSpeed = 5f;

    private Animator animator; 
    private bool isFollowing = false;
    private int boxCounter = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFollowing)
        {
            FollowPlayer();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("rust box")) 
        {
            boxCounter++;
            if (boxCounter >= 6)
            {
                StartCoroutine(StartBossFight()); 
            }
        }
    }    // coroutine for the boss attack 
    private IEnumerator StartBossFight()
    {
        animator.SetTrigger("FlyUp"); // start fly up animation
        yield return new WaitForSeconds(1f);

        isFollowing = true; 
        yield return new WaitForSeconds(1f); 

        for (int i = 0; i < 3; i++) //boss attacks three times
        {
            animator.SetTrigger("Scanning"); // start scanning animation
            yield return new WaitForSeconds(attackDelay); 

            animator.SetTrigger("Throwing"); // start throwing animation
            Attack(); // throw spear at the player
            yield return new WaitForSeconds(attackDelay);
        }

    }

    //  following the player horizontally
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