using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public List<WeaponController> weapons_Unlocked;
    public WeaponController[] total_Weapons;

    [HideInInspector]
    public WeaponController current_Weapon;
    private int current_Weapon_Index;
    private TypeControlAttack current_Type_Control;


    private PlayerArmController[] armController;
    private PlayerAnimations playerAnim;
    private bool isShooting;
    public GameObject meleeDamagePoint;

    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnimations>();

        LoadActiveWeapons();

        current_Weapon_Index = 1;
    }


    // Use this for initialization
    void Start () {
        armController = GetComponentsInChildren<PlayerArmController>();

        
        ChangeWeapon(weapons_Unlocked[1]); //this is because the player starts with a pistol in his hand; Sets the first weapon to be pistol



        playerAnim.SwitchWeaponAnimation(
            (int) weapons_Unlocked[current_Weapon_Index].defaultConfig.typeWeapon
            ); // this is cast into an integer 

	}

    void LoadActiveWeapons()
    {
        weapons_Unlocked.Add(total_Weapons[0]); //first weapon (bat) will be unlocked no matter what to give the player a weapon


        for(int i = 1; i< total_Weapons.Length; i++)
        {
            weapons_Unlocked.Add(total_Weapons[i]);
        }


    }


    public void SwitchWeapon()
    {
        current_Weapon_Index++; //we want the next weapon
        current_Weapon_Index=
        (current_Weapon_Index >= weapons_Unlocked.Count) ? 0 : current_Weapon_Index; // ? checks if the condition in the brackets is true. If so, sets it to 0. Otherwise index out of bounds exception

        playerAnim.SwitchWeaponAnimation(
            (int) weapons_Unlocked[current_Weapon_Index].defaultConfig.typeWeapon);

         ChangeWeapon(weapons_Unlocked[current_Weapon_Index]);
    }

    void ChangeWeapon(WeaponController newWeapon)
    {
        if (current_Weapon) 
         current_Weapon.gameObject.SetActive(false); //if we got the current weapon active, deactivate it

            current_Weapon = newWeapon;
            current_Type_Control = newWeapon.defaultConfig.typeControl;

        newWeapon.gameObject.SetActive(true);

        if (newWeapon.defaultConfig.typeWeapon == TypeWeapon.TwoHand) //for changing from one hand to two hand
        {

            for (int i = 0; i < armController.Length; i++)
            {
                armController[i].ChangeToTwoHand(); //change to two hand sprite
            }
        }
        else
        {
            for (int i = 0; i < armController.Length; i++)
            {
                armController[i].ChangeToOneHand(); //change to one hand sprite
            }
        }
        

        }

    public void Attack()
    {
        if (current_Type_Control == TypeControlAttack.Hold)
        {
            current_Weapon.CallAttack();

        }
        else if (current_Type_Control == TypeControlAttack.Click)
        {
            if (!isShooting) //If you're not shooting while holding a click weapon, shoot
            {
                current_Weapon.CallAttack();
                isShooting = true;
            }
        }

    }

    public void ResetAttack()
    {
        isShooting = false;
    }


    void AllowCollisionDetection() //for melee
    {
        
        meleeDamagePoint.SetActive(true);

    }

    void DenyCollisionDetection() //for melee
    {
        
        meleeDamagePoint.SetActive(false);

    }




}



