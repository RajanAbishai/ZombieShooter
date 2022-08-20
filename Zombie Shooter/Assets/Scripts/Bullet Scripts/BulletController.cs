using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [HideInInspector]
    public int damage;
    private float speed=60f;

    //private float default_Time_Alive = 2f;
    private WaitForSeconds wait_For_Time_Alive = new WaitForSeconds(2f);

    private IEnumerator coroutineDeactivate; //create a coroutine as a variable and simply assign it to a coroutine function to activate and deactivate coroutine
    private Vector3 direction; //for left or right because of the side Tommy faces when firing to move the bullet in that direction. Should not move the bullet to the left side when tommy is facing the right side or vice versa

    public GameObject rocket_Explosion; //for rocket explosion




	// Use this for initialization
	void Start () {

        if (this.tag == TagManager.ROCKET_MISSILE_TAG)
        {
            speed = 8f; //speed of the projectile?
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(direction * speed * Time.deltaTime); //Time.deltaTime for smoother movement
	}


     void OnEnable()
    {
        coroutineDeactivate = WaitForDeactivate();
        StartCoroutine(coroutineDeactivate);

    }

     void OnDisable()
    {
        if (coroutineDeactivate != null)
        {
            StopCoroutine(coroutineDeactivate);
        }
        
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    IEnumerator WaitForDeactivate()
    {
        yield return wait_For_Time_Alive; //wait_For_Time_Alive is an object, ns
        gameObject.SetActive(false);


    }

    public void ExplosionFX()
    {
        AudioManager.instance.FenceExplosion();
        Instantiate(rocket_Explosion, transform.position, Quaternion.identity);
    }


} 
