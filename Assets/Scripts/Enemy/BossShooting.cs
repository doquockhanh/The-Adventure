using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject bullet;
    public Transform bulletPos;
    public float timer;
    public GameObject player;
    public float chase;
    public float timerShoot;
    private Animator anim;
    private BossMoving bossMoving;
    private bool isDead = false;
    private Stats PlayerDame;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        bossMoving = GetComponent<BossMoving>();
        anim.SetBool("Ismoving", true);
        rb = GetComponent<Rigidbody2D>();
        PlayerDame= player.GetComponent<Stats>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (isDead) return; 

        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < chase)
        {
            timer += Time.deltaTime;
            if (timer > timerShoot)
            {
                bossMoving.FlipTowardsTarget(player.transform.position);
                timer = 0;
                anim.SetTrigger("Attack");
            }
        }
    }

    public void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stats BossHealth = transform.GetComponent<Stats>();
        
        if (collision.collider.CompareTag("AttackArea"))
        {
            BossHealth.TakeDamage(PlayerDame.damage);
           if(BossHealth.heath <=0)
            {
                anim.SetBool("Death", true);
                isDead = true;
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                bossMoving.enabled = false;
                this.enabled = false;
                Destroy(gameObject, 2f);
            }
        }
    }
}