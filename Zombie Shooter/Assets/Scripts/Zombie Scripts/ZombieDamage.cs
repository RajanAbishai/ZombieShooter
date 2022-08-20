using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamage : MonoBehaviour {

    public LayerMask collisionLayer;
    public float radius = 1f;
    public int damage = 3;

    private bool playerDead;

   

    void Update () {

        if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.PLAYER) //this is added to avoid null reference exception
        {
            AttackPlayer();
        }

        if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.FENCE) ////this is added to avoid null reference exception
        {
            AttackFence();
        }


        
	}

    void AttackPlayer() 
    {
        if (GameplayController.instance.playerAlive)
        {
            //this creates a circle at given radius and position and this is the collision layer. If game objects overlap in this layer, it is a
            //collision

            Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);
            if (target) //to prevent null refernece exception when the player runs away from an attack
            {
                if (target.tag == TagManager.PLAYER_HEALTH_TAG) //adding something called player tag instead of player health tag.. removing
                {
                    //print("Attacked player");
                    target.GetComponent<PlayerHealth>().DealDamage(damage);
                }

            }
        }

        
    }
   
    void AttackFence() {

        if (!GameplayController.instance.fenceDestroyed) //default of fenceDestroyed is false
        {
            Collider2D target = Physics2D.OverlapCircle(transform.position, radius, collisionLayer);
            


            if (target.tag == TagManager.FENCE_TAG)
            {
                target.GetComponent<FenceHealth>().DealDamage(damage); // access the DealDamage function from the FenceHealth script
                
            }
        }

    }


}


