using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    #region Variables
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction cancel, assignItem1, assignItem2;
    private static bool exists;
    private string currentScene;
    private PlayerUI playerUI;
    [SerializeField] PauseGame pauseGame;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] AudioSource openMenu;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject pauseFirstButton, inventoryFirstButton;
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
        var uiActionMap = inputMaster.FindActionMap("UI");

        cancel = uiActionMap.FindAction("Cancel");
        assignItem1 = uiActionMap.FindAction("AssignItem1");
        assignItem2 = uiActionMap.FindAction("AssignItem2");
    }

    void Start()
    {
        ChangeCurrentScene();
        playerUI = FindObjectOfType<PlayerUI>();
        //changes the amt of keys shown in the UI depending on scene (Will add more with more dungeons)
        if (CurrentScene.Contains("Dungeon"))
        {
            //gets the last index (which will be the number of the dungeon)
            char dungeonNum = CurrentScene[CurrentScene.Length - 1];
            Debug.Log(dungeonNum);
            //converts the char to int
            playerUI.ChangeKeyCountText(int.Parse(dungeonNum.ToString()));
        }
        else
        {
            playerUI.ChangeKeyCountText(-1);
        }
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        FindItemToUse(context);
    }

    private void FindItemToUse(InputAction.CallbackContext context)
    {
        Debug.Log(context.action.name);
        if (Time.timeScale != 0 && context.action.name == "UseItem1")
        {
            if (inventoryManager.usableItems.myInventory[0] != null)
                inventoryManager.usableItems.myInventory[0].Use();
        }
        else if (Time.timeScale != 0 && context.action.name == "UseItem2")
        {
            if (inventoryManager.usableItems.myInventory[1] != null)
                inventoryManager.usableItems.myInventory[1].Use();
        }
    }

    //Used in PlayerController to access PauseGame method.
    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        PauseGame();
    }

    //Pause the game by changing timeScale, reducing volume, opening pause panel and disabling PlayerController script to stop movement
    private void PauseGame()
    {
        if(!isPaused)
        {
            pauseGame.Pause(true);
            isPaused = true;
            pauseScreen.SetActive(true);
            optionsScreen.SetActive(false);

            cancel.performed += OpenPauseMenu;
            cancel.Enable();

            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }
        else
        {
            pauseGame.UnPause();
            isPaused = false;
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(false);

            cancel.performed -= OpenPauseMenu;
            cancel.Disable();
        }
    }

    //Used in PlayerController to access InventoryScreen method.
    public void OpenInventoryMenu(InputAction.CallbackContext context)
    {
        InventoryScreen();
    }

    //almost the same as the PauseGame() method but will be opening a different screen.
    private void InventoryScreen()
    {
        if (!inventoryOpen)
        {
            pauseGame.Pause(true);
            inventoryManager.SetTextAndButton("", "", null, null);
            inventoryOpen = true;
            openMenu.Play();
            inventoryScreen.SetActive(true);

            assignItem1.performed += inventoryManager.AssignItem;
            assignItem2.performed += inventoryManager.AssignItem;
            cancel.performed += OpenInventoryMenu;
            assignItem1.Enable();
            assignItem2.Enable();
            cancel.Enable();

            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(inventoryFirstButton);
        }
        else
        {
            pauseGame.UnPause();
            inventoryOpen = false;
            inventoryScreen.SetActive(false);

            assignItem1.performed -= inventoryManager.AssignItem;
            assignItem2.performed -= inventoryManager.AssignItem;
            cancel.performed -= OpenInventoryMenu;
            assignItem1.Disable();
            assignItem2.Disable();
            cancel.Disable();
        }
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

    public bool InventoryOpen
    {
        get { return inventoryOpen; }
    }
    #endregion
}
