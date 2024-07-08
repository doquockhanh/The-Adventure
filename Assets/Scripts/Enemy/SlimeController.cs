using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    private Stats stats;
    public GameObject enemyPrefab;
    public GameObject sprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        if (TryGetComponent(out stats))
        {
            stats.OnDeath += OnDeath;
        }
    }


    // Update is called once per frame
    void Update()
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
            Flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            Flip();
            currentPoint = pointB.transform;
        }
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    public void OnDeath(Stats stats)
    {
        Destroy(gameObject);
        SpawnEnemies();
    }
    public void SpawnEnemies()
    {
        int numberOfEnemiesToSpawn = 2;
        float randomX = transform.position.x - 1;
        float randomX2 = transform.position.x + 1;

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(randomX, randomX2), -0.284f, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Stats playerStats = collision.GetComponent<Stats>();
            if (playerStats != null && stats != null)
            {
                playerStats.TakeDamage(stats.damage);
            }
        }
    }
}
