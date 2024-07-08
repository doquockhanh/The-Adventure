using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController_Bat : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject player;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    public float chaseSpeed;
    public float chaseRange = 10f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("Ismoving", true);
        gameObject.GetComponent<Stats>().OnDeath += EnemyDie;
    }
    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }
    private void Patrol()
    {
     
            Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
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


    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            Stats EnemyStats = transform.GetComponent<Stats>();
            Stats PlayerStats = collision.GetComponent<Stats>();
            if (PlayerStats != null && EnemyStats != null)
            {
                PlayerStats.TakeDamage(EnemyStats.damage);
            }
           

        }
    }
    public void EnemyDie(Stats stats)
    {
        Destroy(gameObject);   
        
    }
}
