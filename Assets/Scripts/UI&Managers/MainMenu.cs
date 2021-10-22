using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    #region Variables
    [SerializeField] private Button startButton;
    #endregion

    #region Unity Methods

    //loads the first scene of the game
    public void StartGame() 
    {
        SceneManager.LoadScene("Scene_1");
    }

    //exits the application.
    public void ExitGame() 
    {
        Application.Quit();
    }

    #endregion
}
