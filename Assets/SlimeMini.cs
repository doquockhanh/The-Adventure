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
        StartCoroutine(CheckFollow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CheckFollow()
    {
        yield return new WaitForSeconds(4f);
        Instantiate(particlePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}