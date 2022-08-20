using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkBounds : MonoBehaviour {

    public float min_X, max_X;

    
    //Values to be assigned in the inspector
    
    //min_X=-22.0f;
    //max_X=145.8f;





    void Update () {
        MovementBounds();
	}

    void MovementBounds()
    {
        Vector3 temp = transform.position;

        //we can't go below the minimum x position

        if (temp.x < min_X)
        {
            temp.x = min_X; 
        }

        //we can't go above the maximum x position
        if (temp.x > max_X)
        {
            temp.x = max_X; 
        }

        transform.position = temp;

    }






}
