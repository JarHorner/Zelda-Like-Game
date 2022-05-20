using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseScreen : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject optionsMenu;
    public GameObject optionsFirstButton, optionsClosedButton;

    #endregion

    #region Methods

    public void SaveGame()
    {

    }

    public void OptionsMenu()
    {
        optionsMenu.GetComponent<Animator>().SetBool("IsActive", true);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptionsMenu()
    {
        optionsMenu.GetComponent<Animator>().SetBool("IsActive", false);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

    //exits application
    public void ExitGame() 
    {
        SaveGame();
        SceneManager.LoadScene("MainMenu");
        FindObjectOfType<PauseGame>().UnPause();
        this.gameObject.SetActive(false);
        Destroy(FindObjectOfType<PlayerController>().gameObject);
    }
    #endregion
}
