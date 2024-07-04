using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject sprite;
    void Start()
    {
    
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Die();
        }
    }

    void Die()
    {
        SpawnEnemies();
        Destroy(gameObject);
    }
    void SpawnEnemies()
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