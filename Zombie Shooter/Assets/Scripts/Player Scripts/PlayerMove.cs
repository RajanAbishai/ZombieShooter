using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Rigidbody2D myBody;
    private float moveForce_X = 1.5f, moveForce_Y=1.5f;

    private PlayerAnimations PlayerAnimation; //object creation?
	
    //Awake is the first function called when the game runs

	void Awake () {
        myBody = GetComponent<Rigidbody2D>(); //way to get the game objects on which we have attached the component
        //This is how we get components from our game object on which we attached those components

        PlayerAnimation = GetComponent <PlayerAnimations>();
	}

    void FixedUpdate()
    {
        Move();
    }


    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); //for horizontal axis
        float v = Input.GetAxisRaw("Vertical"); //for vertical axis

        //print("GET AXIS: "+ Input.GetAxisRaw("Horizontal"));

        if (h > 0) {

            myBody.velocity = new Vector2(moveForce_X, myBody.velocity.y); //we are only altering the x velocity
        }

        else if (h < 0) {

            myBody.velocity = new Vector2(-moveForce_X, myBody.velocity.y); //we are only altering the x velocity
        }

        else {

            myBody.velocity = new Vector2(0, myBody.velocity.y); //we are only altering the x velocity
        }

        //now for vertical axis

        if (v > 0)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, moveForce_Y); //we are only altering the y velocity
        }

        else if (v < 0)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, -moveForce_Y); //we are only altering the y velocity
        }

        else
        {
            myBody.velocity = new Vector2(myBody.velocity.x, 0);
        }

        //ANIMATE

        if (myBody.velocity.x != 0 || myBody.velocity.y != 0)
        {
            PlayerAnimation.PlayerRunAnimation(true);
        }
        else if (myBody.velocity.x == 0 && myBody.velocity.y == 0)
        {
            PlayerAnimation.PlayerRunAnimation(false);
        }

            Vector3 tempScale = transform.localScale; //to make him face the direction he is moving to

        if (h > 0)
        {
            tempScale.x = -1f;
        }

        else if (h < 0)
        {
            tempScale.x = 1f;
        }

        transform.localScale = tempScale;
    }

}
