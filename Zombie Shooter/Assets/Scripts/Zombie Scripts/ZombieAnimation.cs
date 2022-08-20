using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Awake () {
        anim = GetComponent <Animator>();

	}
	
    public void Attack()
    {
        anim.SetTrigger(TagManager.ATTACK_PARAMETER); //when attacking (attack is ticked), attack animation occurs

    }

    public void Hurt()
    {
        anim.SetTrigger(TagManager.GET_HURT_PARAMETER); //when the get hurt parameter is fulfilled, the get hurt animation plays
    }

    public void Dead()
    {
        
        anim.SetTrigger(TagManager.DEAD_PARAMETER); //when dead, the dead animation plays
    }

}
