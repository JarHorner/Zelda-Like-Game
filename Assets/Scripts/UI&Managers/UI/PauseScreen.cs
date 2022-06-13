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
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Animator gameSavedNoticeAnim;
    public GameObject optionsFirstButton, optionsClosedButton;

    #endregion

    #region Methods

    public void SaveGame() 
    {
        SaveSystem.SavePlayer(FindObjectOfType<PlayerController>(), FindObjectOfType<CameraController>(), FindObjectOfType<InventoryManager>());
        gameSavedNoticeAnim.SetTrigger("Fade");
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
        SaveSystem.SavePlayer(FindObjectOfType<PlayerController>(), FindObjectOfType<CameraController>(), FindObjectOfType<InventoryManager>());
        uiManager.DeactivatePauseScreen();
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
