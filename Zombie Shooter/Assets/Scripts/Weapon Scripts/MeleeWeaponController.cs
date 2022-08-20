using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : WeaponController { //inherits from weaponcontroller

    public override void ProcessAttack()
    {
        //base.ProcessAttack();
        AudioManager.instance.MeleeAttackSound();
     
    }







} // class
