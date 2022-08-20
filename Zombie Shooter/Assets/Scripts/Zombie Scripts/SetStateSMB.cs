using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStateSMB : StateMachineBehaviour {

    public int numberAnimationRandom;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        int randState = Random.Range(1, numberAnimationRandom + 1); //If the +1 isn't there,  the value of number animation random is set to 4 in the inspector is because it includes the first number but not the last number
        animator.SetInteger(TagManager.RANDOM_PARAMETER, randState);
    }



}
