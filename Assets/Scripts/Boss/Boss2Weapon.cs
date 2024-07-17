using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2Weapon : MonoBehaviour
{
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;
    private Stats PlayerHealth;
    private Stats BossDame;
    private float BossDamageEange;
    public float helathToEnraged;
    private void Start()
    {
        PlayerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        BossDame = GameObject.FindGameObjectWithTag("Boss2").GetComponent<Stats>();
        BossDamageEange = BossDame.damage * 2;
    }
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange,attackMask);
        if (colInfo != null )
        {
            if (colInfo.CompareTag("Player"))
            {
                PlayerHealth.TakeDamage(BossDame.damage);
            }
        }
        if(BossDame.heath <= 0)
        {
            Destroy(gameObject, 2f);
        }


    }
    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null && colInfo.CompareTag("Player"))
        {
            if (colInfo.CompareTag("Player"))
            {
                PlayerHealth.TakeDamage(BossDamageEange);
            }
        }
        if (BossDame.heath <= 0)
        {
            Destroy(gameObject);
        }

    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
    

}
