using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject sprite;
    private Stats stats;
    void Start()
    {
    
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            Stats playerStats = collision.collider.GetComponent<Stats>();
            if (playerStats != null && stats != null)
            {
                playerStats.TakeDamage(stats.damage);
            }
        }
    }

    void Die()
    {
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
}