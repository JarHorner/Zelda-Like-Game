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

    public void StartGame() 
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void CreditMenu() 
    {
        
    }

    #endregion
}
