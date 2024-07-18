using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMecha : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerController playerController;
    public float speedBullet;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Follow();   
    }
    private void Follow()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 moveToPlayer = Vector2.MoveTowards(transform.position, playerPosition, speedBullet * Time.fixedDeltaTime);
        transform.position = moveToPlayer;
       
    }

}
