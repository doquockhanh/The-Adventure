using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMini : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particlePrefab;
    public float destroyAfter = 4f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Instantiate(particlePrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}