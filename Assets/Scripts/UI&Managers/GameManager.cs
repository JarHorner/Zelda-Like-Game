using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Variables
    
    private UIManager uiManager;
    private GameObject player;
    private Animator playerAnimator;
    private SoundManager soundManager;
    private AudioSource bgMusic;
    [SerializeField] AudioSource openMenu;
    private bool isPaused = false;
    private bool inventoryOpen = false;
    
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        player = GameObject.FindWithTag("Player");
        playerAnimator = player.GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    // Update is called once per frame
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
                uiManager.getPauseScreen().SetActive(false);
            }
            else
            {
                Pause(true);
                isPaused = true;
                uiManager.getPauseScreen().SetActive(true);
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
                uiManager.getInventoryScreen().SetActive(false);
            }
            else
            {
                Pause(true);
                inventoryOpen = true;
                openMenu.Play();
                uiManager.getInventoryScreen().SetActive(true);
            }
        }
    }

    //pauses the the timescale of the game is true, if false, only the player (good for cutscene-ish stuff)
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

    #endregion
}
