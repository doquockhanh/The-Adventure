using System.Collections;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    public bool isAngry = false;
    public Vector2 limitMove1;
    public Vector2 limitMove2;
    public float jumpForce = 5f;
    public float exp = 1f;
    private bool isMoving = true;
    private bool inGround = true;
    private float lastRandom = 1f;
    private Rigidbody2D rb;
    private Animator animator;
    private Stats stats;
    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (TryGetComponent(out stats))
        {
            stats.OnDeath += OnDeath;
        }

        StartCoroutine(RandomSide());
        StartCoroutine(RandomMove());
    }
    void FixedUpdate()
    {
        if (isMoving && inGround)
        {
            animator.SetTrigger("jump");
            Move();
        }
        else { };
    }

    private void Move()
    {
        if (rb == null)
        {
            return;
        }
        // flip 
        if (lastRandom < 0 && transform.localScale.x > 0 || lastRandom >= 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        // jump 
        float angle = transform.localScale.x > 0 ? 45f : 90f;
        Vector2 jumpDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        rb.velocity = jumpDirection * jumpForce;

        // set after jump
        inGround = false;
    }

    private IEnumerator RandomSide()
    {
        while (true)
        {
            lastRandom = Random.Range(-1f, 1f);
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator RandomMove()
    {
        while (true)
        {
            isMoving = Random.Range(-1f, 1f) > 0f;
            yield return new WaitForSeconds(3f);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            inGround = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Stats playerStats = other.collider.GetComponent<Stats>();
            if (playerStats != null && stats != null)
            {
                playerStats.TakeDamage(stats.damage);
            }
        }
    }

    public void OnDeath(Stats stats)
    {
        StopAllCoroutines();
        player.GetComponent<Stats>().SetExp(exp);
        Destroy(gameObject.GetComponent<Stats>());

        // game object destroy after this anim end
        animator.SetTrigger("die");

        // hoac neu khong the thi tu dong destroy sau 2s
        Destroy(gameObject, 2f);
    }
}
