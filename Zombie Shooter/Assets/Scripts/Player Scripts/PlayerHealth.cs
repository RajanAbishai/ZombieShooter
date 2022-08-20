using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health = 100;
    public GameObject[] bloodFX;
    private PlayerAnimations playerAnim;


	void Awake () {
        //Get Component in parent because Tommy is the parent and Health is the child
        playerAnim = GetComponentInParent<PlayerAnimations>();
	}
	
	
    public void DealDamage(int damage)
    {
        health -= damage;
        GameplayController.instance.PlayerLifeCounter(health);

        //playerAnim.Hurt();

        if (health <= 0)
        {
           

            GameplayController.instance.playerAlive = false;

            GetComponent<Collider2D>().enabled = false;
            playerAnim.Dead();
            bloodFX[Random.Range(0, bloodFX.Length)].SetActive(true); //bloodFX.Length is used instead of 2 because we have 2 elements in the array. Useful incase we need more elements in the future

            GameplayController.instance.GameOver(); //displays the game over panel when the player dies

        }
    }


}
