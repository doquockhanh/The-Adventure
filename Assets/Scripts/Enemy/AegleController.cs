using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

enum AegleStatus
{
    moving = 1,
    following = 2,
    attacking = 3
}

public class AegleController : MonoBehaviour
{
    public float flyHeight = 10f;
    public LayerMask groundLayer;
    public float speed = 5f;
    public float speedY = 1f;
    public float attackRate = 1f;
    private bool moveRight = false;
    private AegleStatus status = AegleStatus.moving;
    public float attackRange = 7f;
    public float rangeCanFollow = 15f;
    public float diveSpeed = 5f;
    private Vector2 attackPoint;
    private Transform player;
    private Animator animator;
    private Stats stats;
    private Rigidbody2D rb;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (TryGetComponent(out stats))
        {
            stats.OnDeath += OnDeath;
            stats.OnTakeDamage += OnTakeDame;
        }

        StartCoroutine(RandomDirection());
        StartCoroutine(RandomFollow());
        StartCoroutine(RandomAttack());
        StartCoroutine(ResetVelocity());
    }

    void FixedUpdate()
    {
        switch (status)
        {
            case AegleStatus.moving:
                {
                    // Dieu chinh do cao bay ve flyheight
                    AdjustFlyheight();
                    Move();
                    break;
                }
            case AegleStatus.following:
                {
                    // Dieu chinh do cao bay ve flyheight
                    AdjustFlyheight();
                    Following();
                    break;
                }
            case AegleStatus.attacking:
                {

                    Attacking();
                    break;
                }
            default:
                {
                    Debug.Log("no vao default");
                    break;
                }
        }

    }

    private void Move()
    {
        if (moveRight && transform.localScale.x < 0 || !moveRight && transform.localScale.x > 0)
        {
            // flip
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // di chuyen dua theo huong cua localScale
        float moveX = speed * (-transform.localScale.x / Mathf.Abs(transform.localScale.x)) * Time.fixedDeltaTime;
        transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
    }

    private void Following()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) < 3f)
        {
            return;
        }

        if (transform.position.x > player.position.x && transform.localScale.x < 0
                || transform.position.x < player.position.x && transform.localScale.x > 0)
        {
            // flip
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // di chuyen dua theo huong cua localScale
        float moveX = speed * (-transform.localScale.x / Mathf.Abs(transform.localScale.x)) * Time.fixedDeltaTime;
        transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
    }

    private void Attacking()
    {
        transform.position = Vector2.MoveTowards(transform.position, attackPoint, diveSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, attackPoint) < 0.1f)
        {
            AttackDone();
        }
    }

    private void AttackDone()
    {
        animator.SetBool("diving", false);
        // attack xong, bay ve lai
        status = AegleStatus.moving;
    }

    private IEnumerator RandomDirection()
    {
        while (true)
        {
            if (status == AegleStatus.attacking)
                yield return new WaitForSeconds(Random.Range(3f, 6f));

            moveRight = !moveRight;
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }
    }

    private IEnumerator RandomFollow()
    {
        while (true)
        {
            if (status == AegleStatus.attacking)
                yield return new WaitForSeconds(3f);

            if (Vector2.Distance(transform.position, player.position) <= rangeCanFollow && Random.Range(-1f, 1f) > 0)
            {
                status = AegleStatus.following;
            }
            else
            {
                status = AegleStatus.moving;
            }
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator RandomAttack()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, player.position) <= attackRange && Random.Range(-1f, 1f * attackRate) > 0)
            {
                attackPoint = player.position;
                animator.SetBool("diving", true);
                // flip
                if (transform.position.x > player.position.x && transform.localScale.x < 0
                        || transform.position.x < player.position.x && transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
                status = AegleStatus.attacking;
            }
            else
            {
                status = AegleStatus.moving;
            }

            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator ResetVelocity() {
        while (true)
        {
            if(status == AegleStatus.moving || status == AegleStatus.following) {
                rb.velocity = Vector3.zero;
            }

            yield return new WaitForSeconds(3f);
        }
    }

    private void AdjustFlyheight()
    {
        float currentFlyHeight = CalculateDistanceToGround();


        // Dieu chinh do cao bay ve flyheight
        if (currentFlyHeight != -1f && currentFlyHeight >= 0)
        {
            float distanceDifference = flyHeight - currentFlyHeight;

            Vector3 newPosition = transform.position;
            newPosition.y += distanceDifference * speedY * Time.fixedDeltaTime;
            transform.position = newPosition;
        }

    }

    float CalculateDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayer);

        if (hit.collider != null)
        {
            return hit.distance;
        }
        else
        {
            return -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (status == AegleStatus.attacking)
        {
            AttackDone();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Stats playerStats = collision.collider.GetComponent<Stats>();
            if (playerStats != null && stats != null)
            {
                playerStats.TakeDamage(stats.damage);
            }
        }
    }

    public void OnDeath(Stats stats)
    {
        Destroy(gameObject);
    }

    public void OnTakeDame(Stats stats)
    {
        animator.SetTrigger("hurt");
    }
}
