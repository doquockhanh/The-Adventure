using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWhenAnim : StateMachineBehaviour
{
    public PlayerAttack playerAttack;
    private bool functionCalled = false;
    private void Awake()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }
  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (!functionCalled && stateInfo.normalizedTime >= 0.7f)
        {
            functionCalled = true;
            playerAttack.Attack();
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        functionCalled = false;
    }
}
