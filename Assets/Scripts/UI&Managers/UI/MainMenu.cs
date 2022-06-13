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
    private bool startingNewGame;

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

    public void NewGame(bool startingNewGame)
    {
        Debug.Log("NewGame");
        this.startingNewGame = startingNewGame;
    }

    //loads the first scene of the game
    private void StartGame() 
    {
        SaveSystem.LoadedGame = false;
        SceneManager.LoadScene("OverWorld");
        if(GameObject.Find("Player(Clone)") != null)
            GameObject.Find("Player(Clone)").GetComponent<PlayerController>().enabled = true;
    }

    public void StartORLoadGame(string fileName)
    {
        SaveSystem.currentFileName = fileName;
        if (startingNewGame)
        {
            StartGame();
        }
        else
        {
            SaveSystem.currentPlayerData = SaveSystem.LoadPlayer();
            if (SaveSystem.currentPlayerData != null)
            {
                SaveSystem.LoadedGame = true;
                SceneManager.LoadScene(SaveSystem.currentPlayerData.lastScene);
                if(GameObject.Find("Player(Clone)") != null)
                    GameObject.Find("Player(Clone)").GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    //exits the application.
    public void ExitGame() 
    {
        Application.Quit();
    }

    public void ActivateMenu(GameObject panel)
    {
        Debug.Log("ActivateMenu");
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
            if (startingNewGame)
            {
                newGameButton.GetComponent<Button>().enabled = false;
                newGameButton.GetComponent<Image>().enabled = false;
                newGameButton.GetComponent<Animator>().SetBool("Selected", true);
            }
            else
            {
                loadGameButton.GetComponent<Button>().enabled = false;
                loadGameButton.GetComponent<Image>().enabled = false;
                loadGameButton.GetComponent<Animator>().SetBool("Selected", true);
            }

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("SaveFile1").gameObject);
        }
        title.SetActive(false);
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

            if (startingNewGame)
            {
                newGameButton.GetComponent<Button>().enabled = true;
                newGameButton.GetComponent<Image>().enabled = true;
                newGameButton.GetComponent<Animator>().SetBool("Selected", false);
            }
            else
            {
                loadGameButton.GetComponent<Button>().enabled = true;
                loadGameButton.GetComponent<Image>().enabled = true;
                loadGameButton.GetComponent<Animator>().SetBool("Selected", false);
            }
        }
        title.SetActive(true);
        exitGameButton.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    #endregion
}
