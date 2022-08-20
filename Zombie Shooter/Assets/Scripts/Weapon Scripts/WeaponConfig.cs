using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeControlAttack
{
    Click,
    Hold
}

public enum TypeWeapon
{
    Melee, 
    OneHand,
    TwoHand
}
[System.Serializable] /* makes this visible in the inspector panel*/
public struct DefaultConfig {

    public TypeControlAttack typeControl; //sets the first one in the enum
    public TypeWeapon typeWeapon; //sets the first one in the enum

    [Range(0,100)] /*adds a range*/
    public int damage;

    [Range(0,100)]
    public int criticalDamage;

    [Range(0.01f, 1.0f)]
    public float fireRate;

    [Range(0,100)]
    public int criticalRate;



}
