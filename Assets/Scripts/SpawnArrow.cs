using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject arrowPrefab;
    public Transform posSpawn;
    void Start()
    {
        InvokeRepeating("Spawn", 1, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        Instantiate(arrowPrefab, posSpawn.position, Quaternion.identity);
    }
}
