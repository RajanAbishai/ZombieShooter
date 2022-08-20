using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour {


    public Sprite one_Hand_Sprite, two_Hand_Sprite;
    private SpriteRenderer sr;

	// Use this for initialization
	void Awake () {
        sr = GetComponent<SpriteRenderer>();

	}
	
    public void ChangeToOneHand()
    {
        sr.sprite = one_Hand_Sprite; //change the current sprite to one hand sprite
    }

	public void ChangeToTwoHand()
    {
        sr.sprite = two_Hand_Sprite; //change the current sprite to two hand sprite
    }
}
