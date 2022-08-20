using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    private ZombieMovement zombie_Movement;
    private ZombieAnimation zombie_Animation;

    private Transform targetTransform;
    private bool canAttack;
    private bool zombie_Alive;
    public GameObject damage_Collider;
    public int zombieHealth = 10;

    public GameObject[] fxDead;


    private float timerAttack;

    private int fireDamage = 10;
    public GameObject coinCollectable;



	// Use this for initialization
	void Start () {

        zombie_Movement = GetComponent<ZombieMovement>();
        zombie_Animation = GetComponent<ZombieAnimation>();

        zombie_Alive = true;


        if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.PLAYER)
        {
            targetTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
        }

        else if (GameplayController.instance.zombieGoal == GameplayController.ZombieGoal.FENCE)
        {
            GameObject[] fences = GameObject.FindGameObjectsWithTag(TagManager.FENCE_TAG);

            targetTransform = fences[Random.Range(0, fences.Length)].transform;
        }



    }
	
	
	void Update () {
        if (zombie_Alive)
        {
            CheckDistance(); //Check distance between player and zombie
        }

	}

    void CheckDistance() { 
    

        if (targetTransform) { 
            
                if (Vector3.Distance(targetTransform.position, transform.position)> 1.5f) { //if distance between player & zombie is greater than 1.5 unity units.. 5.0f makes the zombie stay 5 units away

                    zombie_Movement.Move(targetTransform); //movement


                }
            else {

                if (canAttack)
                {
                    zombie_Animation.Attack();

                    timerAttack += Time.deltaTime;

                    if (timerAttack > 0.45f) //play the audio only every 0.45 seconds during the attack, not continuously
                    {
                        timerAttack = 0f;
                        AudioManager.instance.ZombieAttackSound();

                    }
                }

            }


            }
        }
     
    public void ActivateDeadEffect(int index) //head falling off and blood
    {
        fxDead[index].SetActive(true);
        
        
            if (fxDead[index].GetComponent<ParticleSystem>())
            {
                fxDead[index].GetComponent<ParticleSystem>().Play();
            }
        
    }

    IEnumerator DeactivateZombie()
    {

        AudioManager.instance.ZombieDieSound();
        yield return new WaitForSeconds(2f); //2f original

        //The following condition was added because it interfered with the zombie's deadbody disappearing (made inactive)

        if(GameplayController.instance.gameGoal==GameplayController.GameGoal.KILL_ZOMBIES) {

            GameplayController.instance.ZombieDied(); //call this when the zombie is killed ... add condition if gamemode is kill zombies
        }


        //Randomizing coin spawning

        if (Random.Range(0, 10) > 6) 
        {
            Instantiate(coinCollectable, transform.position, Quaternion.identity);
        }


        
        gameObject.SetActive(false);
        
    }

    public void DealDamage(int damage) //this is for melee damage
    {

        if (zombieHealth > 0) //added this to avoid repeating animation
        {

            zombie_Animation.Hurt();
            zombieHealth -= damage;


            if (zombieHealth <= 0)
            {
                
                zombie_Alive = false;
                zombie_Animation.Dead(); //this seems to work
                StartCoroutine(DeactivateZombie());
            }
        }

        /*if (zombieHealth <= 0)
        {
            print("Inside zombieHealth<=0"); //not entering this area at all. Entered when using melee
            zombie_Alive = false;
            zombie_Animation.Dead(); //this seems to work
            StartCoroutine(DeactivateZombie());
        }*/




    }

    void ActivateDamagePoint()
    {
        damage_Collider.SetActive(true);
    }

    void DeactivateDamagePoint()
    {
        damage_Collider.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D target) //to trigger zombie attacks
    {
        if(target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            canAttack = true;
        }

        if (target.tag == TagManager.BULLET_TAG || target.tag == TagManager.ROCKET_MISSILE_TAG )
        {
            if (zombieHealth > 0)
            { //put all in an if
                zombie_Animation.Hurt();

                zombieHealth -= target.gameObject.GetComponent<BulletController>().damage;

                if (target.tag == TagManager.ROCKET_MISSILE_TAG) //zombie health condition added
                {
                    target.gameObject.GetComponent<BulletController>().ExplosionFX();
                }

                if (zombieHealth <= 0)
                {
                    zombie_Alive = false;
                    zombie_Animation.Dead();

                    StartCoroutine(DeactivateZombie());
                    target.gameObject.SetActive(false); //closed temporarily
                }

            }//closing if for zombiehealth>0

            //target.gameObject.SetActive(false); //closed because the deactivate zombie thing didn't seem to work
        }

        if (target.tag == TagManager.FIRE_BULLET_TAG)
        {
            if (zombieHealth > 0) //added to fix repetition
            {  
                zombie_Animation.Hurt();
                zombieHealth -= fireDamage;

                if (zombieHealth <= 0)
                {
                    zombie_Alive = false;
                    zombie_Animation.Dead();

                    StartCoroutine(DeactivateZombie());
                }
            }
        }


    }

    void OnTriggerExit2D(Collider2D target) //to make the zombie stop attacking once out of range
    {
        if (target.tag == TagManager.PLAYER_HEALTH_TAG || target.tag == TagManager.PLAYER_TAG || target.tag == TagManager.FENCE_TAG)
        {
            canAttack = false;
        }
    }





} //class
