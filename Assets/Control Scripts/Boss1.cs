using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public Transform player; 
    public GameObject spearPrefab; 
    public float speed = 10f; 
    public float attackDelay = 2f;
    public float spearSpeed = 20f;
    public Transform bone_1; 
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
            if (boxCounter >= 2)
            {
                StartCoroutine(StartBossFight()); 
            }
        }
    }    // coroutine for the boss attack 
    private IEnumerator StartBossFight()
    {
        Debug.Log("Boss Fight Started");
        
        animator.SetBool("FlyUp",true); // Start Fly Up animation
        yield return new WaitForSeconds(10f);
        animator.SetBool("FlyUp", false); 
        Debug.Log("FlyUp animation completed");

        isFollowing = true;
        Debug.Log("Boss is now following the player");
        yield return new WaitForSeconds(10f);

        for (int i = 0; i < 3; i++) // Boss attacks three times
        {
            Debug.Log($"Boss Attack #{i + 1}");

            
            animator.SetBool("Scanning",true); // Start Scanning animation
            yield return new WaitForSeconds(2f);
            animator.SetBool("Scanning", false);
            yield return new WaitForSeconds(5f);
            animator.SetBool("Throwing",true); // Start Throwing animation
            Attack(); // Throw spear at the player
            yield return new WaitForSeconds(5f);

            animator.SetBool("Throwing", false);
             // Delay between attacks
            animator.SetBool("isFollowing", true);
            yield return new WaitForSeconds(10f);
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

        GameObject spear = Instantiate(spearPrefab, Vector3.zero, Quaternion.identity);
        spear.transform.localPosition= bone_1.transform.position;
        Vector2 direction = (player.position - spear.transform.position).normalized;
        spear.GetComponent<Rigidbody2D>().velocity = direction * spearSpeed;
    }
}