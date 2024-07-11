using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWhenAnimStateEnter : StateMachineBehaviour
{
    public BossShooting Shoot;
    private bool functionCalled = false;
    private void Awake()
    {
        Shoot = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossShooting>();
    }
  
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (!functionCalled && stateInfo.normalizedTime >= 0.7f)
        {
            functionCalled = true;
            Shoot.shoot();
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        functionCalled = false;
    }
}
