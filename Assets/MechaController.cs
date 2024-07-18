using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaController : MonoBehaviour
{
    public Transform targetBullet;
    public GameObject bulletPrefab;
    private Animator anm;
    void Start()
    {
        InvokeRepeating("SpawnBullet", 2, 2);
        anm.SetTrigger("Fire");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void SpawnBullet()
    {

        Instantiate(bulletPrefab, targetBullet.position, Quaternion.identity);
    }
}
