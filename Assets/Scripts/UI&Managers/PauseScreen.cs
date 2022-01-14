using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject optionsMenu;

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
    }

    //exits application
    public void ExitGame() 
    {
        Application.Quit();
    }
    #endregion
}
