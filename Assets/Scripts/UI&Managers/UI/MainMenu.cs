using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    #region Variables
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject loadGameButton;
    [SerializeField] private GameObject optionsButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject exitGameButton;
    [SerializeField] private GameObject player;

    #endregion

    #region Unity Methods

    private void Start() 
    {
        ControlScheme.GetControlSchemes();
    }

    void Update() 
    {
        ControlScheme.GetUsedControlScheme();
    }

    //loads the first scene of the game
    public void StartGame() 
    {
        string sceneName = SceneTracker.LastSceneName;
        SceneManager.LoadScene(sceneName);
        if(GameObject.Find("Player(Clone)") != null)
            GameObject.Find("Player(Clone)").GetComponent<PlayerController>().enabled = true;
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
            loadGameButton.SetActive(false);
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
            loadGameButton.SetActive(false);
            creditsButton.GetComponent<Button>().enabled = false;
            creditsButton.GetComponent<Image>().enabled = false;
            creditsButton.GetComponent<Animator>().SetBool("Selected", true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("CreditsExitButton").gameObject);
        }
        else if (panel.name == "LoadGamePanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", true);
            creditsButton.SetActive(false);
            optionsButton.SetActive(false);
            loadGameButton.GetComponent<Button>().enabled = false;
            loadGameButton.GetComponent<Image>().enabled = false;
            loadGameButton.GetComponent<Animator>().SetBool("Selected", true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("LoadGameExitButton").gameObject);
        }
        title.SetActive(false);
        newGameButton.SetActive(false);
        exitGameButton.SetActive(false);
    }

    public void DeactivateMenu(GameObject panel)
    {
        if (panel.name == "OptionsPanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", false);
            creditsButton.SetActive(true);
            loadGameButton.SetActive(true);
            optionsButton.GetComponent<Button>().enabled = true;
            optionsButton.GetComponent<Image>().enabled = true;
            optionsButton.GetComponent<Animator>().SetBool("Selected", false);
        }
        else if (panel.name == "CreditsPanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", false);
            optionsButton.SetActive(true);
            loadGameButton.SetActive(true);
            creditsButton.GetComponent<Button>().enabled = true;
            creditsButton.GetComponent<Image>().enabled = true;
            creditsButton.GetComponent<Animator>().SetBool("Selected", false);
        }
        else if (panel.name == "LoadGamePanel")
        {
            panel.GetComponent<Animator>().SetBool("IsActive", false);
            creditsButton.SetActive(true);
            optionsButton.SetActive(true);
            loadGameButton.GetComponent<Button>().enabled = true;
            loadGameButton.GetComponent<Image>().enabled = true;
            loadGameButton.GetComponent<Animator>().SetBool("Selected", false);
        }
        title.SetActive(true);
        newGameButton.SetActive(true);
        exitGameButton.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    #endregion
}
