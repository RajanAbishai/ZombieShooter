using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeaponController : WeaponController
{ //GunWeaponController inherits from WeaponController.. all public variables and functions are inherited


    public Transform spawnPoint; //spawn point for the bullet and the effect of the muzzleflash other particle effects
    public GameObject bulletPrefab;
    public ParticleSystem fx_Shot;
    public GameObject fx_BulletFall;

    private Collider2D fireCollider;

    private WaitForSeconds wait_Time = new WaitForSeconds(0.02f);
    private WaitForSeconds fireColliderWait = new WaitForSeconds(0.02f);

    // Use this for initialization
    void Start()
    {
        if (!GameplayController.instance.bullet_And_BulletFX_Created)
        {
            GameplayController.instance.bullet_And_BulletFX_Created = true;

            if (nameWp != NameWeapon.FIRE && nameWp!=NameWeapon.ROCKET) 
            {
           SmartPool.instance.CreateBulletAndBulletFall(bulletPrefab, fx_BulletFall, 100); //we create 100 bullets and 100 fall effects


            }

        }


       if(!GameplayController.instance.rocket_Bullet_Created)
        {
            

            if (nameWp == NameWeapon.ROCKET)
            {
                GameplayController.instance.rocket_Bullet_Created = true;
                SmartPool.instance.CreateRocket(bulletPrefab, 100); //the bullet prefab selected for tommy is the rocket bullet.

            }



        }


        if (nameWp == NameWeapon.FIRE)
        {
            fireCollider = spawnPoint.GetComponent<BoxCollider2D>();
        }


    }

    public override void ProcessAttack() //overriding previously created virtual function that was created in parent class
    {
        //base.ProcessAttack();

        switch (nameWp) {

            case NameWeapon.PISTOL:
                AudioManager.instance.GunSound(0);
                break;


        
            case NameWeapon.MP5:
                AudioManager.instance.GunSound(1);
                break;

            case NameWeapon.M3:
                AudioManager.instance.GunSound(2);
                break;

            case NameWeapon.AK:
                AudioManager.instance.GunSound(3);
                break;

            case NameWeapon.AWP:
                AudioManager.instance.GunSound(4);
                break;


            case NameWeapon.FIRE:
                AudioManager.instance.GunSound(5);
                break;

            case NameWeapon.ROCKET:
                AudioManager.instance.GunSound(6);
                break;


        } // switch and case

        //Spwan Bullet
        if ((transform != null && nameWp!=NameWeapon.FIRE)) //when the player dies, we deactivate the transform
        {
            if (nameWp != NameWeapon.ROCKET)
            {
                GameObject bullet_FallFX = SmartPool.instance.SpawnBulletFX(spawnPoint.transform.position, Quaternion.identity);
                //scale it based on whether we are facing left side or right side
                bullet_FallFX.transform.localScale = (transform.root.eulerAngles.y>1.0f)? 
                    new Vector3(-1f,1f,1f): new Vector3(1f,1f,1f);


                //transform.root returns the top most transform. Changing the transform x from 1 to -1 changes the direction

                StartCoroutine(WaitForShootEffect());

            }
            //usually whenthe transform scale at x is set to 1,  the game object is facing the right side. This is why -transform.root.localscale.x is used
            SmartPool.instance.SpawnBullet(spawnPoint.transform.position, new Vector3(-transform.root.localScale.x, 0f, 0f), spawnPoint.rotation, nameWp);
                



        }
        else
        {
            StartCoroutine(ActiveFireCollider());
        }



    } // Process Attack

    IEnumerator WaitForShootEffect() {
        yield return wait_Time;
        fx_Shot.Play();
    }

    IEnumerator ActiveFireCollider()
    {
        fireCollider.enabled = true; //enabling fire collider
        fx_Shot.Play();

        yield return fireColliderWait;
        fireCollider.enabled = false; //disabling fire collider
    }
}