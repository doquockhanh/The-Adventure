using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] arrowPrefab;
    public Transform posSpawn;
    void Start()
    {
        InvokeRepeating("Spawn", 0, 0.9f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Spawn()
    {
        int RandomArrow = Random.Range(0, arrowPrefab.Length);
        Instantiate(arrowPrefab[RandomArrow], posSpawn.position, Quaternion.identity);
    }
}
