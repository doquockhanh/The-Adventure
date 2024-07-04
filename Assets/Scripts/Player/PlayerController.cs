using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private LayerMask groundPlayer;
    [SerializeField] private LayerMask wallPlayer;
    public Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    public float jump;
    private BoxCollider2D boxCollider;
    private bool facingRight = true;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Move(horizontalInput);
        
        if(Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    void Move(float horizontalInput)
    {

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jump);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    public bool isGrounded()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundPlayer);
        return raycasthit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycasthit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallPlayer);
        return raycasthit.collider != null;
    }
    public bool canAttack()
    {
        return true;
    }

    public void onLevelUp() {
        Debug.Log("level Up");
    }

    public void OnDie() {
        Debug.Log("die");
        Destroy(gameObject);
    }
}
