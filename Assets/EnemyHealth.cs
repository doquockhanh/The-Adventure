using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn upon death

    // This function is called when the enemy takes damage
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

       
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-3.984f, 3.984f), -0.284f, 0);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}