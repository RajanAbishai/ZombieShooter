using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceHealth : MonoBehaviour {


    public int health = 100;
    public ParticleSystem wood_Break_FX, wood_Explode_FX;
    


	public void DealDamage(int damage)
    {
        health -= damage;
        wood_Break_FX.Play();

        //



        if (health <= 0)
        {
            
            wood_Explode_FX.Play(); //explosion once the fence is destroyed
            AudioManager.instance.FenceExplosion();
            GameplayController.instance.fenceDestroyed = true; //This applies to all fences. when one fence gets destroyed, game is over.


            StartCoroutine(DeactivateGameObject());
            GameplayController.instance.GameOver();
        }
    }

    IEnumerator DeactivateGameObject()
    {
        yield return new WaitForSeconds(0.2f); //wait for 0.2 seconds
        gameObject.SetActive(false);
      
    }


} //class
