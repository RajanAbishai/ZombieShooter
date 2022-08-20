using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMZombieDead : StateMachineBehaviour {

    public int index; //this is to denote if it's index 1, 2 or 3 

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.GetComponent<ZombieController>().ActivateDeadEffect(index); //head falling off and blood

    }



}
