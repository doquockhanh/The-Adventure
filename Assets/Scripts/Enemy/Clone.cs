using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SlimeClone : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    private Rigidbody2D rb;
    private GameObject target;
    private Stats stats;
    public GameObject deathParticlePrefab;
    private float lifeTime = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindTarget();
        if (TryGetComponent(out stats))
        {
            stats.OnDeath += OnDeath;
        }
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
        StartCoroutine(DieAfterDelay(4.0f));
    }
    public void FlipThisSprite()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        target = null;
        if (collision.gameObject.CompareTag("Player"))
        {
            Stats playerStats = collision.collider.GetComponent<Stats>();
            if (playerStats != null && stats != null)
            {
                playerStats.TakeDamage(stats.damage);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FlipThisSprite();
        }

    }
    public void OnDeath(Stats stats)
    {

    }
    private IEnumerator DieAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Die();
    }
    void Die()
    {
        // Sinh Particle System tại vị trí của kẻ địch
        GameObject Clone = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        Destroy(Clone, lifeTime);
        // Hủy kẻ địch
        Destroy(gameObject);
    }
}

