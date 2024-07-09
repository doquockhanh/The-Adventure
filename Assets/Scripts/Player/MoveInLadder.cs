
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_jump : MonoBehaviour
{
    private bool onLadder;
    private Rigidbody2D rb;
    private float inputVertical;
    public float speed = 0;
    private float defaultSpeed = 3f;
    public float gravityScale = 0f;
    public GameObject player;
    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        if (speed <= 0) {
            float playerSpeed = player.GetComponent<PlayerController>().speed;
            speed = playerSpeed <= 0 ? defaultSpeed : playerSpeed;
        }
        gravityScale = player.transform.GetComponent<Rigidbody2D>().gravityScale;
    }

    void FixedUpdate()
    {
        inputVertical = Input.GetAxisRaw("Vertical");
        if (onLadder)
        {
            ClimbLadder();
        }
    }
    void ClimbLadder()
    {
        if (rb == null) return;
        rb.velocity = new Vector2(rb.velocity.x, inputVertical * speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            if(onLadder == false) {
                anim.SetBool("inLadder", true);
            }
            onLadder = true;
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            if(onLadder == true) {
                anim.SetBool("inLadder", false);
            }
            onLadder = false;
            rb.gravityScale = gravityScale;
        }
    }
}