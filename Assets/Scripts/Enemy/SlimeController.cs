using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

enum SlimeStatus
{
    moving = 1,
    follow = 2,
    idle = 3,
}

public class SlimeController : MonoBehaviour
{
    public float speed = 3f;
    public float speedMini = 5f;
    public float followDistance = 10f;
    public bool canFollow = true;
    public bool isSelfDestroy = false;
    public float movingRange = 5f;
    private SlimeStatus status = SlimeStatus.moving;
    private GameObject player;
    private float initPosition;
    private bool isMovingRight = true;
    private Stats stats;
    public GameObject slimeMiniPrefab;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(CheckFollow());
        if (isSelfDestroy)
        {
       
        }
        initPosition = transform.position.x;
        if (TryGetComponent(out stats))
        {
            stats.OnDeath += OnDeath;
        }
    }

    void FixedUpdate()
    {
        switch (status)
        {
            case SlimeStatus.idle:
                {
                    break;
                }
            case SlimeStatus.follow:
                {
                    Follow();
                    break;
                }
            case SlimeStatus.moving:
                {
                    CheckMoveOutRange();
                    Moving();
                    break;
                }
        }
    }

    private void Follow()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 moveToPlayer = Vector2.MoveTowards(transform.position, playerPosition, speedMini * Time.fixedDeltaTime);
        transform.position = moveToPlayer;
        FlipToTarget(player.transform);
    }

    private void Moving()
        {
        if (isMovingRight)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            float moveX = speed * Time.fixedDeltaTime;
            transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
        }
        else
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            float moveX = speed * Time.fixedDeltaTime;
            transform.position = new Vector3(transform.position.x - moveX, transform.position.y, transform.position.z);
        }
        }

    void CheckMoveOutRange()
    {
        if (transform.position.x > initPosition + movingRange || transform.position.x < initPosition - movingRange)
        {
            isMovingRight = transform.position.x < initPosition;
        }
        }

    void FlipToTarget(Transform target)
    {
        if (transform.localScale.x > 0 && target.position.x < transform.position.x
            || transform.localScale.x < 0 && target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    public void OnDeath(Stats stats)
    {
        Destroy(gameObject.GetComponent<Stats>());
        StopAllCoroutines();
        Destroy(gameObject, 2f);

    }
    void Destroy()
    {
        Destroy(gameObject);
        StartCoroutine(SpawnMiniSlime());
    }

    IEnumerator CheckFollow()
    {
        while (true)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (canFollow && distance <= followDistance)
    {
                status = SlimeStatus.follow;
        }
            else
        {
                status = SlimeStatus.moving;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Stats playerStats = other.collider.GetComponent<Stats>();
            if (playerStats != null && stats != null)
            {
                playerStats.TakeDamage(stats.damage);
            }
        }
    }

    IEnumerator SpawnMiniSlime()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("SDsadas");
        int numberOfEnemiesToSpawn = 3;
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
    {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            Instantiate(slimeMiniPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
