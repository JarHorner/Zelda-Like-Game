using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Variables
    private UIManager uiManager;
    private SoundManager soundManager;
    private AudioSource source;
    private GameObject playerController;
    private bool isPaused = false;
    private bool inventoryOpen = false;
    
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
        playerController = GameObject.FindWithTag("Player");
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
        source = soundManager.GetComponentInChildren<AudioSource>();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                source.volume = 0.2f;
                uiManager.pauseScreen.SetActive(false);
                playerController.GetComponent<PlayerController>().enabled = true;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
                source.volume = 0.02f;
                uiManager.pauseScreen.SetActive(true);
                playerController.GetComponent<PlayerController>().enabled = false;
            }
        }
    }

    //almost the same as the PauseGame() method but will be opening a different screen.
    private void InventoryScreen()
    {
        source = soundManager.GetComponentInChildren<AudioSource>();
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                Time.timeScale = 1;
                inventoryOpen = false;
                source.volume = 0.2f;
                uiManager.inventoryScreen.SetActive(false);
                playerController.GetComponent<PlayerController>().enabled = true;
            }
            else
            {
                Time.timeScale = 0;
                inventoryOpen = true;
                source.volume = 0.02f;
                uiManager.inventoryScreen.SetActive(true);
                playerController.GetComponent<PlayerController>().enabled = false;
            }
        }
    }

    #endregion
}
