using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;
    [SerializeField] private HealthManager healthManager;

    public void SaveGame()
    {
        //PlayerPrefs.SetInt("PlayerCurrHp", healthManager.getCurrentHealth());
        //PlayerPrefs.SetInt("Dungeon1Open", uIManager.isDungeon1Opened());
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
