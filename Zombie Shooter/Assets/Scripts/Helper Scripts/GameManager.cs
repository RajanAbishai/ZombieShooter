using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameObject[] characters;

    [HideInInspector]
    public int character_Index;

      void Awake()
    {
        MakeSingleton();
    }

     void Start()
    {
        character_Index = 0;
    }

     void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;    //subscribe and see 
    }


    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;    //unsubscribe
    }




    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject); //destroy the duplicate
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }


    }



    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {

        if (scene.name != TagManager.MAIN_MENU_NAME) //this is to check if we have loaded one of the game scenes instead of the main menu
        {
            if (character_Index != 0)
            {
                GameObject tommy = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);

                Instantiate(characters[character_Index], tommy.transform.position,Quaternion.identity);

                tommy.SetActive(false); //or we can destroy him

                //Destroy(tommy); 
            }
        }


    }

}
