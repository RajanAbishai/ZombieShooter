using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum NameWeapon  //what is in this enum determines the order of the weapons that were allotted assets under the weapon manager script
{
    MELEE,
    PISTOL,
    MP5,
    M3,
    AK,
    AWP,
    FIRE,
    ROCKET
}

public class WeaponController : MonoBehaviour {

    public DefaultConfig defaultConfig;
    public NameWeapon nameWp;

    protected PlayerAnimations playerAnim;
    protected float lastShot;

    public int gunIndex;
    public int currentBullet;
    public int bulletMax;

    private void Awake()
    {
        playerAnim = GetComponentInParent<PlayerAnimations>();
        currentBullet = bulletMax;
    }
    
    public void CallAttack()
    {
        //we will check fire rate, when can we attack, etc

        if(Time.time > lastShot + defaultConfig.fireRate) //this means we shot and can we shoot again.. this is for the firerate. Last Shot is the last shot time
        {
            if (currentBullet > 0)
            {
                ProcessAttack();
                //animate shooting

                playerAnim.AttackAnimation();

                lastShot = Time.time; // here, we are memorizing the last time we took a shot
                currentBullet--; // so that we can subtract from the current bullet that we got
            }

            else
            {
                //PLAY NO Ammo sound

            }
        }
    }

    public virtual void ProcessAttack() // This is virtual because it is going to be overridden in the next class for the weapon
    {

    }



} //class
