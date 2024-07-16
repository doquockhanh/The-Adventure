using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSpawnBoss : MonoBehaviour
{ 
    public GameObject bossPrefab; 
    public Transform spawnPoint; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnBoss();
            Destroy(gameObject);
        }
    }

    void SpawnBoss()
    {
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

