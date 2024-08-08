using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MechaCombatStatus
{
    Fire = 1,
    Punch = 2,
    Laze = 3,
    Lazy = 0,
    Move = 4
}
public class MechaController : MonoBehaviour
{
    public Transform targetBullet;
    public GameObject bulletPrefab;
    private Animator anm;

    public float hoverHeight = 2f; // Độ cao mong muốn cách mặt đất
    public float hoverForce = 5f; // Lực để giữ cho nhân vật nổi
    public float speed = 3f; // Tốc độ di chuyển của nhân vật
    public LayerMask groundLayer; // Lớp mặt đất để kiểm tra va chạm
    private Rigidbody2D rb;
    private bool moveRight = true;
    public int poolSize = 5; // Kích thước bộ đệm
    private List<GameObject> bulletPool;
    private MechaCombatStatus mechaCombatStatus = MechaCombatStatus.Move; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        StartCoroutine(RandomDirection());
        StartCoroutine(RandomStatus());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (mechaCombatStatus)
        {
            case MechaCombatStatus.Move:
                {
                    Move();
                    break;
                }
            case MechaCombatStatus.Fire:
                {
                    mechaCombatStatus = MechaCombatStatus.Move;
                    Fire();
                    break;
                }
            case MechaCombatStatus.Lazy:
                {

                    break;
                }
            case MechaCombatStatus.Laze:
                {
                    mechaCombatStatus = MechaCombatStatus.Move;
                    Fire();
                    break;
                }
            case MechaCombatStatus.Punch:
                {
                    mechaCombatStatus = MechaCombatStatus.Move;
                    Fire();
                    break;
                }
            default:
                {
                    Debug.Log("no vao default");
                    break;
                }
        }
    }

    private void Move()
    {
        if (moveRight && transform.localScale.x > 0 || !moveRight && transform.localScale.x < 0)
        {
            // flip
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // di chuyen dua theo huong cua localScale
        float moveX = speed * (transform.localScale.x / Mathf.Abs(transform.localScale.x)) * Time.fixedDeltaTime;
        transform.position = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
    }

    void Fire()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                bulletPool[i].transform.position = transform.Find("SpawnBullet").position;
                bulletPool[i].transform.rotation = Quaternion.identity;
                bulletPool[i].SetActive(true);
                //anm.SetTrigger("Fire");
                return;
            }
        }

    }
    private IEnumerator RandomDirection()
    {
        while (true)
        {
            moveRight = !moveRight;
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }
    }

    private IEnumerator RandomStatus()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (mechaCombatStatus == MechaCombatStatus.Lazy) continue;

            MechaCombatStatus[] numbers = { MechaCombatStatus.Laze, MechaCombatStatus.Punch, MechaCombatStatus.Move, MechaCombatStatus.Fire };

            int randomIndex = Random.Range(0, numbers.Length);

            mechaCombatStatus = numbers[randomIndex];
            Debug.Log(mechaCombatStatus);
        }
    }
}
