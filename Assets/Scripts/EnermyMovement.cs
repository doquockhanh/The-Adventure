using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed = 3f;
    private Rigidbody2D myRigidbody; 
    private Animator anim; 
    

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        gameObject.GetComponent<Stats>().OnDeath += EnemyDie;
        FlipDirection(); 

    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(speed, 0f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Waypoint"))
        {
            FlipDirection();
        }
    }

    void FlipDirection()
    {
        speed = -speed; 
        FlipEnemyFacing(); 
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
    public void EnemyDie(Stats stats)
    {
        Destroy(gameObject);
    }

    
}

   

   
