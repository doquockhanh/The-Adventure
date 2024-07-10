using System.Collections;
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
    public float followDistance = 10f;
    public bool canFollow = true;
    public bool isSelfDestroy = false;
    public float destroyAfter = 5f;
    public float movingRange = 5f;
    private SlimeStatus status = SlimeStatus.moving;
    private GameObject player;
    private float initPosition;
    private bool isMovingRight = true;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(CheckFollow());
        if (isSelfDestroy)
        {
            Destroy(gameObject, destroyAfter);
        }
        initPosition = transform.position.x;
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
        Vector2 moveToPlayer = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.fixedDeltaTime);
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
            float moveX = speed  * Time.fixedDeltaTime;
            transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
        }else {
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
        if(transform.localScale.x > 0 && target.position.x < transform.position.x 
            || transform.localScale.x < 0 && target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    IEnumerator CheckFollow()
    {
        while (true)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if(canFollow && distance <= followDistance)
            {
                status = SlimeStatus.follow;
            }else
            {
                status = SlimeStatus.moving;
            }
            yield return new WaitForSeconds(2f);
        }
    }
    
}
