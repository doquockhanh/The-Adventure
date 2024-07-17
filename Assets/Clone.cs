using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeClone : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    private Rigidbody2D rb;
    private GameObject target;
 
    private int health = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindTarget();
    }
    void FindTarget()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            Vector3 posY = new Vector3(transform.position.x, -0.2149f, 0);
            Vector3 pos = Vector3.MoveTowards(posY, target.transform.position, speed * Time.deltaTime);
            rb.MovePosition(pos);
        }
    }
    public void FlipThisSprite()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        target = null;
        health++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            FlipThisSprite();
        }
    }
}

