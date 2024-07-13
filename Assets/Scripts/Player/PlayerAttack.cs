using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
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
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (anim != null)
            {
                anim.SetTrigger("attack");
            }
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

    public void Attack()
    {
        attacking = true;
        if (attackArea != null)
        {
            attackArea.SetActive(true);
        }
    }
}