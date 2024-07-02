using System;
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
    public GameObject attackArea;
    private bool attacking = false;
    private float TimeToAttack = 0.5f;
    private float timer = 0f;

    private void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
    }
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
        if (attacking)
        {
            timer = Time.deltaTime;
            if(timer > TimeToAttack )
            {
                timer = 0f;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }
    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }
}
