using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour {

    public static SmartPool instance; //it will create a contructor or object out of this class

    private List<GameObject> bullet_Fall_Fx = new List<GameObject>(); // this is where we store bullet fall effects
    private List<GameObject> bullet_Prefabs = new List<GameObject>();
    private List<GameObject> rocket_Bullet_Prefabs = new List<GameObject>();

    //we are going to strole all of these prefabs in our list

    public GameObject[] zombies;
    private float y_Spawn_Pos_Min = -3.7f, y_Spawn_Pos_Max = -0.36f;
    private Camera mainCamera;


    void Awake () {
        MakeInstance();
	}

    private void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("StartSpawningZombies", 1f, Random.Range(1f, 5f)); // calls it after 1 second and then, calls every 1-5 seconds
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this; //this refers to the class SmartPool or the instance or an object from the class itself
        }
    }

    private void OnDisable()
    {
        instance = null;
    }

    



    void Update () {
		

	}


   public void CreateBulletAndBulletFall(GameObject bullet, GameObject bulletFall, int count) //count keeps track of the number of bullets and bullet falls to be created
    {

        /* Creating a copy of the game objects being passed into the parameter, storing them in their appropriate lists and deactivating them
         because we don't need them active if we don't shoot*/

        for (int i = 0; i < count; i++)
        {
            GameObject temp_Bullet = Instantiate(bullet);
            GameObject temp_Bullet_Fall = Instantiate(bulletFall); //same as ctrl+D, ns

            bullet_Prefabs.Add(temp_Bullet); //to store it in the list (resizable array)
            bullet_Fall_Fx.Add(temp_Bullet_Fall);


            bullet_Prefabs[i].SetActive(false); 
            bullet_Fall_Fx[i].SetActive(false);



        } //Create bullet and bullet fall

        

    }

    public void CreateRocket(GameObject rocket, int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject temp_Rocket_Bullet = Instantiate(rocket);

            rocket_Bullet_Prefabs.Add(temp_Rocket_Bullet);

            rocket_Bullet_Prefabs[i].SetActive(false);
        }
    } // create rocket

    public GameObject SpawnBulletFX(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < bullet_Fall_Fx.Count; i++)
        {
            if (!bullet_Fall_Fx[i].activeInHierarchy) 
            {
                /*if the bullet is not active in the hierarchy. They were false initially. Now, we activate them. We set it to the position and rotation 
                  passed in the function*/

                bullet_Fall_Fx[i].SetActive(true);
                bullet_Fall_Fx[i].transform.position = position; 
                bullet_Fall_Fx[i].transform.rotation = rotation;

                return bullet_Fall_Fx[i]; //return the element at index i
            }
        }
        return null; /* Because if by any case if we have some bullet that is not active, then, return null. Otherwise, we get an error saying
       that not all code paths return a value*/

    } //Spawn bullet fall effects

    public void SpawnBullet(Vector3 position, Vector3 direction, Quaternion rotation, NameWeapon weaponName)
    {
        if (weaponName != NameWeapon.ROCKET) //if we are not shooting rockets
        {
            for (int i = 0; i < bullet_Prefabs.Count; i++) //We search for the first bullet that is not active in our game and activate it
            {
                if (!bullet_Prefabs[i].activeInHierarchy)
                {
                    bullet_Prefabs[i].SetActive(true);
                    bullet_Prefabs[i].transform.position = position;
                    bullet_Prefabs[i].transform.rotation = rotation;

                    //Get the bullet script

                    bullet_Prefabs[i].GetComponent<BulletController>().SetDirection(direction); //BulletControllerScript


                    //Set bullet damage
                    SetBulletDamage(weaponName, bullet_Prefabs[i]);
                    break; //to break outside of the loop and therefore, not spawn too many bullets (or rather all the bullets)

                }

            }


        }

        else   //for rockets
        {
            for(int i = 0; i < rocket_Bullet_Prefabs.Count; i++)
            {

                if (!rocket_Bullet_Prefabs[i].activeInHierarchy)
                {
                    rocket_Bullet_Prefabs[i].SetActive(true);
                    rocket_Bullet_Prefabs[i].transform.position = position;
                    rocket_Bullet_Prefabs[i].transform.rotation = rotation;

                    //Get the bullet script

                    rocket_Bullet_Prefabs[i].GetComponent<BulletController>().SetDirection(direction); //SetDirection function from the bullet controller script

                    //Set the bullet damage
                    SetBulletDamage(weaponName, rocket_Bullet_Prefabs[i]);

                    

                    break; //to break outside of the loop and not spawn many rockets (or rather all the rockets, ns)
                }
                    


            }

        }


    }
    

    void SetBulletDamage(NameWeapon weaponName, GameObject bullet)
    {
        switch (weaponName)
        {
            case NameWeapon.PISTOL:
                bullet.GetComponent<BulletController>().damage = 2;
                break;

            case NameWeapon.MP5:
                bullet.GetComponent<BulletController>().damage = 3;
                break;

            case NameWeapon.M3:
                bullet.GetComponent<BulletController>().damage = 4;
                break;


            case NameWeapon.AK:
                bullet.GetComponent<BulletController>().damage = 5;
                break;

            case NameWeapon.AWP:
                bullet.GetComponent<BulletController>().damage = 10;
                break;

            

            case NameWeapon.ROCKET:
                bullet.GetComponent<BulletController>().damage = 10 ;
                break;


            

            

               


        }

    }

    void StartSpawningZombies()
    {
        if(GameplayController.instance.gameGoal==GameplayController.GameGoal.DEFEND_FENCE)
        {
            float xPos = mainCamera.transform.position.x; //getting the main camera's position which is the middle of the screen
            xPos += 15; //moving it to the right side. This is done so that they can't spawn behind the player

            float yPos = Random.Range(y_Spawn_Pos_Min, y_Spawn_Pos_Max);

            Instantiate(zombies[Random.Range(0,zombies.Length)], new Vector3(xPos, yPos, 0), Quaternion.identity ); // 0, zombies.Length is to access the 5 elements

        }


        else if ( GameplayController.instance.gameGoal == GameplayController.GameGoal.KILL_ZOMBIES ||
                  GameplayController.instance.gameGoal == GameplayController.GameGoal.TIMER_COUNTDOWN ||
                  GameplayController.instance.gameGoal == GameplayController.GameGoal.WALK_TO_GOAL_STEPS)
        {
            float xPos = mainCamera.transform.position.x; //getting the main camera's position which is the middle of the screen

            if (Random.Range(0, 2) > 0)
            {
                xPos += Random.Range(10f, 15f); //Spawns in front of the player
            }

            else
            {
                xPos -= Random.Range(10f, 15f);
            }


         float yPos = Random.Range(y_Spawn_Pos_Min, y_Spawn_Pos_Max);

        Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(xPos, yPos, 0), Quaternion.identity); // 0, zombies.Length is to access the 5 elements




        }

        if (GameplayController.instance.gameGoal == GameplayController.GameGoal.GAME_OVER)
        {
            CancelInvoke("StartSpawningZombies");
        }


    }



} //class
