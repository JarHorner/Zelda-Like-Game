using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject optionsMenu;
    private PlayerInput playerInput;
    private InputAction exit, confirm;

    public GameObject optionsFirstButton, optionsClosedButton;

    #endregion

    #region Methods
    
    //will be used later!
    public void SaveGame()
    {
        //PlayerPrefs.SetInt("PlayerCurrHp", healthManager.getCurrentHealth());
        //PlayerPrefs.SetInt("Dungeon1Open", uIManager.isDungeon1Opened());
    }

    public void OptionsMenu()
    {
        optionsMenu.SetActive(true);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    //exits application
    public void ExitGame() 
    {
        Application.Quit();
    }
    #endregion
}
