using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();

    }

    //circle means trigger, checkbox means bool

    public void PlayerRunAnimation(bool run){
        anim.SetBool(TagManager.RUN_PARAMETER, run); //parameters need to be called by their actual names. Capital R in the parameter, so.. Run

}
    public void AttackAnimation()
    {
        anim.SetTrigger(TagManager.ATTACK_PARAMETER);
    }

    //type weapon is for whether it's melee or 1 hand or 2 hand
    public void SwitchWeaponAnimation(int typeWeapon)
    {
        anim.SetInteger(TagManager.TYPE_WEAPON_PARAMETER, typeWeapon); //maybe to switch to the type that was taken as input in typeWeapon 
        anim.SetTrigger(TagManager.SWITCH_PARAMETER);
    }


    public void Hurt()
    {
        anim.SetTrigger(TagManager.GET_HURT_PARAMETER);
    }

    public void Dead()
    {
        anim.SetTrigger(TagManager.DEAD_PARAMETER);
    }










} //class
