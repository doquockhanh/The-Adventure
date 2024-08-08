using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMecha : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedBullet;
    private GameObject player;
    public float speed = 10f;
    private Vector3 direction;
    private Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = (player.transform.position - transform.position).normalized;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = direction.normalized * speed;
    }
    private void Follow()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        if(collision.transform.CompareTag("Ground"))
        {
            gameObject.SetActive(false) ;   
        }
    }

}
