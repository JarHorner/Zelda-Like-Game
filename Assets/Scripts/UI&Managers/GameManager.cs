using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Variables
    private string currentScene;
    private UIManager uIManager;
    private GameObject player;
    private Animator playerAnimator;
    private SoundManager soundManager;
    private AudioSource bgMusic;
    [SerializeField] AudioSource openMenu;
    private bool isPaused = false;
    private bool inventoryOpen = false;
    
    #endregion

    #region Unity Methods

    void Start()
    {
        ChangeCurrentScene();
        uIManager = FindObjectOfType<UIManager>();
        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
        Debug.Log(CurrentScene);
        //changes the amt of keys shown in the UI depending on scene (Will add more with more dungeons)
        if (CurrentScene.Contains("Dungeon"))
        {
            //gets the last index (which will be the number of the dungeon)
            char dungeonNum = CurrentScene[CurrentScene.Length - 1];
            Debug.Log(dungeonNum);
            //converts the char to int
            uIManager.ChangeKeyCountText(int.Parse(dungeonNum.ToString()));
        }
        else
        {
            uIManager.ChangeKeyCountText(-1);
        }
    }

    void Update()
    {
        PauseGame();
        InventoryScreen();
    }

    //Pause the game by changing timeScale, reducing volume, opening pause panel and disabling PlayerController script to stop movement
    private void PauseGame()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isPaused)
            {
                UnPause();
                isPaused = false;
                uIManager.GetPauseScreen().SetActive(false);
            }
            else
            {
                Pause(true);
                isPaused = true;
                uIManager.GetPauseScreen().SetActive(true);
            }
        }
    }

    //almost the same as the PauseGame() method but will be opening a different screen.
    private void InventoryScreen()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                UnPause();
                inventoryOpen = false;
                uIManager.GetInventoryScreen().SetActive(false);
            }
            else
            {
                Pause(true);
                inventoryOpen = true;
                openMenu.Play();
                uIManager.GetInventoryScreen().SetActive(true);
            }
        }
    }

    //pauses the the timescale of the game if true. if false, only the player will be stopped (good for cutscene stuff)
    public void Pause(bool timePause) 
    {
        if (timePause)
        {
            Time.timeScale = 0;
        }
        bgMusic = soundManager.GetComponentInChildren<AudioSource>();
        bgMusic.volume = 0.02f;
        player.GetComponent<PlayerController>().enabled = false;
        playerAnimator.speed = 0;
    }

    //returns the changes during a Pause back to normal
    public void UnPause() 
    {
        bgMusic = soundManager.GetComponentInChildren<AudioSource>();
        Time.timeScale = 1;
        bgMusic.volume = 0.2f;
        player.GetComponent<PlayerController>().enabled = true;
        playerAnimator.speed = 1;
    }

    private void ChangeCurrentScene()
    {
        var scene = SceneManager.GetActiveScene();
        currentScene = scene.name;
    }

    public string CurrentScene
    {
        get { return currentScene; }
    }
    #endregion
}
