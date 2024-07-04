﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerController playerMovement;
    public GameObject attackArea;
    private bool attacking = false;
    private float timeToAttack = 1f;
    private float timer = 1f;


    private void Start()
    {
        if (transform.childCount > 0)
        {
            attackArea = transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.LogError("Không tìm thấy vùng tấn công.");
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Không tìm thấy Animator.");
        }

        playerMovement = GetComponent<PlayerController>();
        if (playerMovement == null)
        {
            Debug.LogError("Không tìm thấy PlayerController.");
        }

    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && playerMovement != null && playerMovement.canAttack())
        {
            Attack();

        }

        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer > timeToAttack)
            {
                timer = 0f;
                attacking = false;
                attackArea.SetActive(false);
            }
        }
    }

    private void Attack()
    {
        if (anim != null)
        {
            anim.SetTrigger("attack");
        }
        attacking = true;
        if (attackArea != null)
        {
            attackArea.SetActive(true);
        }
    }
}