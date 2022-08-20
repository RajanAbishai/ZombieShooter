using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameplayController : MonoBehaviour {


    public enum ZombieGoal
    {
        PLAYER,
        FENCE
    }

    public enum GameGoal
    {
        KILL_ZOMBIES,
        WALK_TO_GOAL_STEPS,
        DEFEND_FENCE,
        TIMER_COUNTDOWN,
        GAME_OVER
    }

    public static GameplayController instance;

    [HideInInspector]
    public bool bullet_And_BulletFX_Created, rocket_Bullet_Created;

    [HideInInspector] 
    public bool playerAlive, fenceDestroyed;

    public ZombieGoal zombieGoal = ZombieGoal.PLAYER; //default goal for the zombie
    public GameGoal gameGoal = GameGoal.DEFEND_FENCE; //default goal of the game

    public int zombie_Count=20; //gotta kill 20 zombies by default
    
    public int timer_Count=100;

    private Transform playerTarget; //we need this to count the number of steps to count the steps backwards or forward depending on whether he is moving forward (positive) or backward (negative)
    private Vector3 player_Previous_Position; //So that we know the previous position of our player and know when he is going backwards or forward. If we got 100 steps to move forward. if we make 10 steps forward (90 steps left) but 5 backward, we got 95 steps

    public int step_Count = 100;  // take 100 steps to win our game
    private int initial_Step_Count; //the code that uses this has been commented out because it does not count steps backward


    private Text zombieCounter_Text, stepCounter_Text, timer_Text;
    private Text missionStatus;

    

    private Image playerLife;

    [HideInInspector]
    public int coinCount;



    public GameObject pausePanel, gameOverPanel;


    private void Awake()
    {
        MakeInstance();
    }

    private void Start()
    {
        playerAlive = true;

        if (gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            playerTarget = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform; //we can get the player's transform so that we know where he is moving
            player_Previous_Position = playerTarget.position;

            initial_Step_Count = step_Count; // Intitial step count means how many steps we need to take to finish our game. When we move, we may increase/decrease this step count
            stepCounter_Text = GameObject.Find("Step Counter").GetComponent<Text>();
            stepCounter_Text.text = step_Count.ToString(); //converts the integer into a string and places it here

            

        }

        if(gameGoal== GameGoal.TIMER_COUNTDOWN || gameGoal==GameGoal.DEFEND_FENCE) //use a timer for both game types
        {
            timer_Text = GameObject.Find("Timer Counter").GetComponent<Text>();
            timer_Text.text= timer_Count.ToString();
            
            InvokeRepeating("TimerCountDown", 0f, 1f); //invokes the method at 0 seconds and repeatedly every 1 second
        }

        if (gameGoal == GameGoal.KILL_ZOMBIES)
        {
            zombieCounter_Text = GameObject.Find("Zombie Counter").GetComponent<Text>();
            zombieCounter_Text.text= zombie_Count.ToString(); 
        }

        playerLife = GameObject.Find("Life Full").GetComponent<Image>();
    
    }

     void Update()
    {
        if (gameGoal == GameGoal.WALK_TO_GOAL_STEPS)
        {
            CountPlayerMovement();
        }
    }
    
    void CountPlayerMovement()
    {
        Vector3 playerCurrentMovement = playerTarget.position;

        float dist = Vector3.Distance(new Vector3(playerCurrentMovement.x, 0f, 0f),  new Vector3 (player_Previous_Position.x, 0f, 0f)); //We are measuring the distance between the player's temporary position and previous position

        if (playerCurrentMovement.x > player_Previous_Position.x) //player moving forward
        {
            if (dist > 1)
            {
                step_Count--;

                if (step_Count <= 0)
                {
                    GameOver();
                }

                player_Previous_Position = playerTarget.position;
            }
        }

        else if (playerCurrentMovement.x < player_Previous_Position.x) //player moving backward
        {
            if (dist > 0.8f)
            {
                step_Count++;

               /* if (step_Count >= initial_Step_Count) //unnecessary as it does not count steps if you go backwards beyond starting point
                {
                    step_Count = initial_Step_Count;
                } */

                player_Previous_Position = playerTarget.position; //stores the target position in the previous position so that we can know the difference between the current and the previous position to calculate if we are moving forward or backward
            }

        }

        stepCounter_Text.text = step_Count.ToString();

    }
        
    

    void OnDisable() //good place to dispose any game object you don't need.
    {
        instance = null;
    }


    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ZombieDied() //to keep track of the number of zombies.. called in Zombie Controller
    {
        zombie_Count--;
        zombieCounter_Text.text = zombie_Count.ToString(); //disabling this allows the bodies to disappear

        if (zombie_Count <= 0)
        {
            
            GameOver();
        }



    }

    public void PlayerLifeCounter(float fillPercentage) //has to be called in player health
    {
        fillPercentage /= 100f; // This is because fill amount is set to 1 rather than 100
        playerLife.fillAmount = fillPercentage;
    }

    public void GameOver()
    {

        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        //the below code throws a null reference exception error if the gameOverPanel is not set active first

        if (playerAlive == false)
        {


            missionStatus = GameObject.FindGameObjectWithTag(TagManager.MISSION_TEXT_TAG).GetComponent<Text>();

            
            missionStatus.text = "Player Died!";
        }


        
        else if (fenceDestroyed)
        {
            missionStatus = GameObject.FindGameObjectWithTag(TagManager.MISSION_TEXT_TAG).GetComponent<Text>();
            missionStatus.text = "Fence Destroyed!";
        }
        
        
        
    }


    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; //to stop the movement of everything

    }

    void TimerCountDown()
    {
        timer_Count--;
        timer_Text.text = timer_Count.ToString();

        if (timer_Count <= 0)
        {
            
            
            CancelInvoke("TimerCountDown");
            GameOver();
        }


    }


    public void ResumeGame()
    {
        
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.MAIN_MENU_NAME);
    }


} // class
