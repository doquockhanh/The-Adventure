using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController_Bat : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    public float speed;
    private float speedOriginal;
    public float chaseSpeed = 6;
    public float chaseRange = 10f;
    public float exp = 1f;
    private float originalChaseRange = 10f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Ismoving", false);
        gameObject.GetComponent<Stats>().OnDeath += EnemyDie;
        originalChaseRange = chaseRange;
        speedOriginal = chaseSpeed;

    }
    private void FixedUpdate()
    {
    if(Vector2.Distance(transform.position, player.transform.position) <= chaseRange) 
        { 
            ChasePlayer();
        }
        
    }
   
    private void ChasePlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
        anim.SetBool("Ismoving", true);
       
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            flip();
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            flip();
        }
       
    }
    private IEnumerator ResetChaseRange()
    {
        chaseRange = 0f;
        chaseSpeed = 0f;

        yield return new WaitForSeconds(0.5f);
        chaseRange = originalChaseRange;
        chaseSpeed = speedOriginal;
        

    }




    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 direction2 = (transform.position - transform.position).normalized;

            Stats EnemyStats = transform.GetComponent<Stats>();
            Stats PlayerStats = collision.GetComponent<Stats>();
            if (PlayerStats != null && EnemyStats != null)
            {
                PlayerStats.TakeDamage(EnemyStats.damage);
            }
            StartCoroutine(ResetChaseRange());


        }
    }
    public void EnemyDie(Stats stats)
    {
        player.GetComponent<Stats>().SetExp(exp);
        Destroy(gameObject);   
    }
}
