using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform playerTransform;
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
      
	}
	
	
	void LateUpdate () {
        
        if(GameplayController.instance.gameGoal!=GameplayController.GameGoal.DEFEND_FENCE && //Don't follow the player with the camera if the goal is to defend the fence or if the game is over
            GameplayController.instance.gameGoal != GameplayController.GameGoal.GAME_OVER)
        {
            if (playerTransform) //if we have the player and did not destroy the player using destroy, follow the player
            {
                Vector3 temp = transform.position;
                temp.x = playerTransform.position.x; //player is only being followed on the x axis
                transform.position = temp;

            }

        }


        


	}





} //class

