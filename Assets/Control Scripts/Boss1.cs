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
    private bool FlyUp = false;
    private bool Throwing = false;
    private bool Scanning = false; 
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
        Debug.Log("trigger Works");
        if (other.CompareTag("rustBox")) 
        {
            boxCounter++;
            if (boxCounter >= 1)
            {
                StartCoroutine(StartBossFight()); 
            }
        }
    }    // coroutine for the boss attack 
    private IEnumerator StartBossFight()
    {
        Debug.Log("Boss Fight Started");

        FlyUp = true;
        animator.SetTrigger("FlyUp"); // Start Fly Up animation
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("FlyUp") &&
                                         animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        FlyUp = false; // stops flyup from repeating 
        Debug.Log("FlyUp animation completed");

        isFollowing = true;
        Debug.Log("Boss is now following the player");

        for (int i = 0; i < 3; i++) // Boss attacks three times
        {
            Debug.Log($"Boss Attack #{i + 1}");

            Scanning = true;
            animator.SetTrigger("Scanning"); // Start Scanning animation
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Scanning") &&
                                             animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            Scanning = false;
            Throwing = true;
            animator.SetTrigger("Throwing"); // Start Throwing animation
            Attack(); // Throw spear at the player
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Throwing") &&
                                             animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

            Throwing = false;
            yield return new WaitForSeconds(attackDelay); // Delay between attacks
        }

        Debug.Log("Boss Fight Ended");
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