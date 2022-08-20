using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour {

     void OnTriggerEnter2D(Collider2D target) //script to detect collision
    {
        if(target.tag==TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG)
        {
            GameplayController.instance.coinCount++;

            gameObject.SetActive(false);
        }
    }
}
