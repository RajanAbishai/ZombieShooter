using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateFX : MonoBehaviour {

	// Called everytime the game object is activated
	void OnEnable () {
       Invoke("DeactivateGameObject",2f); //passed the DeactivateGameObject method after 2 seconds

    }

    

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
        
    }
	
} 
