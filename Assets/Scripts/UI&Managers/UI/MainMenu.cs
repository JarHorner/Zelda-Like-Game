using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    #region Variables
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject exitGameButton;

    #endregion

    #region Unity Methods

    //loads the first scene of the game
    public void StartGame() 
    {
        string sceneName = SceneTracker.LastSceneName;
        SceneManager.LoadScene(sceneName);
    }

    //exits the application.
    public void ExitGame() 
    {
        Application.Quit();
    }

    public void ActivateMenu(GameObject panel)
    {
        if (panel.name == "OptionsPanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", true);
            creditsButton.SetActive(false);
            optionsButton.GetComponent<Button>().enabled = false;
            optionsButton.GetComponent<Image>().enabled = false;
            optionsButton.GetComponent<Animator>().SetBool("Selected", true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("ResolutionDropdown").gameObject);
        }
        else if (panel.name == "CreditsPanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", true);
            optionsButton.SetActive(false);
            creditsButton.GetComponent<Button>().enabled = false;
            creditsButton.GetComponent<Image>().enabled = false;
            creditsButton.GetComponent<Animator>().SetBool("Selected", true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Credits_Exit_Button").gameObject);
        }
        title.SetActive(false);
        startGameButton.SetActive(false);
        exitGameButton.SetActive(false);
    }

    public void DeactivateMenu(GameObject panel)
    {
        if (panel.name == "OptionsPanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", false);
            creditsButton.SetActive(true);
            optionsButton.GetComponent<Button>().enabled = true;
            optionsButton.GetComponent<Image>().enabled = true;
            optionsButton.GetComponent<Animator>().SetBool("Selected", false);
        }
        else if (panel.name == "CreditsPanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", false);
            optionsButton.SetActive(true);
            creditsButton.GetComponent<Button>().enabled = true;
            creditsButton.GetComponent<Image>().enabled = true;
            creditsButton.GetComponent<Animator>().SetBool("Selected", false);
        }
        title.SetActive(true);
        startGameButton.SetActive(true);
        exitGameButton.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(startGameButton);
    }

    #endregion
}
