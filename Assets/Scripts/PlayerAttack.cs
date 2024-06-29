using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    private Animator anim;
    private PlayerController playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack() ) 
        {
            Attack();
            cooldownTimer += Time.deltaTime;
        }
    }
    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }
}
