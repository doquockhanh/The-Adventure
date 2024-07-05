using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f; 
    Rigidbody2D myRigidbody; 
    Animator anim; 

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>(); 
        FlipDirection(); 
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0f); 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Waypoint"))
        {
            FlipDirection(); 
        }
    }

    void FlipDirection()
    {
        moveSpeed = -moveSpeed; 
        FlipEnemyFacing(); 
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }

}

   

   
