using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{

    #region Variables
    private static bool exists;
    private string currentScene;
    private UIManager uIManager;
    private SoundManager soundManager;
    private AudioSource bgMusic;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] AudioSource openMenu;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject inventoryScreen;
    private bool isPaused = false;
    private bool inventoryOpen = false;
    
    #endregion

    #region Unity Methods

    private void Awake() 
    {
        //Singleton Effect
        if (!exists)
        {
            exists = true;
            //ensures same player object is not destoyed when loading new scences
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
    }

    void Start()
    {
        ChangeCurrentScene();
        uIManager = FindObjectOfType<UIManager>();
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

        //if game isnt pasued, and proper button is pressed, uses that items event.
        if (Time.timeScale != 0 && Input.GetButtonDown("UseItem1"))
        {
            if (inventoryManager.usableItems.myInventory[0] != null)
                inventoryManager.usableItems.myInventory[0].Use();
        }
        if (Time.timeScale != 0 && Input.GetButtonDown("UseItem2"))
        {
            if (inventoryManager.usableItems.myInventory[1] != null)
                inventoryManager.usableItems.myInventory[1].Use();
        }
        //ensures assigned an item to use from the inventory manager.
        if (inventoryManager.AssignButtonMenuOpen)
        {
            inventoryManager.AssignToButton();
        }
    }

    //Pause the game by changing timeScale, reducing volume, opening pause panel and disabling PlayerController script to stop movement
    private void PauseGame()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(isPaused)
            {
                UnPause();
                isPaused = false;
                pauseScreen.SetActive(false);
                optionsScreen.SetActive(false);
            }
            else
            {
                Pause(true);
                isPaused = true;
                pauseScreen.SetActive(true);
            }
        }
    }

    //almost the same as the PauseGame() method but will be opening a different screen.
    private void InventoryScreen()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            if (inventoryOpen)
            {
                UnPause();
                inventoryOpen = false;
                //ensures if player closes inventory on popup, it will close as well
                inventoryManager.AssignButtonPopup.SetActive(false);
                inventoryScreen.SetActive(false);
            }
            else
            {
                Pause(true);
                inventoryManager.SetTextAndButton("", "", false, null, null);
                inventoryOpen = true;
                openMenu.Play();
                inventoryScreen.SetActive(true);
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
        soundManager = FindObjectOfType<SoundManager>();
        bgMusic = soundManager.GetComponentInChildren<AudioSource>();
        bgMusic.volume = 0.05f;
        FindObjectOfType<PlayerController>().enabled = false;
        FindObjectOfType<PlayerController>().GetComponent<Animator>().speed = 0;
    }

    //returns the changes during a Pause back to normal
    public void UnPause() 
    {
        soundManager = FindObjectOfType<SoundManager>();
        bgMusic = soundManager.GetComponentInChildren<AudioSource>();
        Time.timeScale = 1;
        bgMusic.volume = 0.2f;
        FindObjectOfType<PlayerController>().enabled = true;
        FindObjectOfType<PlayerController>().GetComponent<Animator>().speed = 1;
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
