using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGameMenu : MonoBehaviour
{
    #region  variables
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private OnScreenKeyboard onScreenKeyboard;
    [SerializeField] private SaveFile file1;
    [SerializeField] private SaveFile file2;
    [SerializeField] private SaveFile file3;
    #endregion

    #region Mathods

    private void Start() 
    {
        file1.FileName.text = PlayerPrefs.GetString(file1.FileName.name);
        file2.FileName.text = PlayerPrefs.GetString(file2.FileName.name);
        file3.FileName.text = PlayerPrefs.GetString(file3.FileName.name);

        AddContentToSaveFile();
    }

    private void Update() {
    }

    public void StartORLoadGame(TMP_Text saveFileName)
    {
        if (mainMenu.StartingNewGame)
        {
            if (saveFileName.text == "")
                onScreenKeyboard.EnterFileName(saveFileName);
            else
                StartGame();
        }
        else
        {
            SaveSystem.CurrentFileName = $"/{saveFileName.text}.SL";
            SaveSystem.CurrentPlayerData = SaveSystem.LoadPlayer();
            if (SaveSystem.CurrentPlayerData != null)
            {
                SaveSystem.LoadedGame = true;
                SceneManager.LoadScene(SaveSystem.CurrentPlayerData.lastScene);
                if(GameObject.Find("Player(Clone)") != null)
                    GameObject.Find("Player(Clone)").GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    //loads the first scene of the game
    private void StartGame() 
    {
        SaveSystem.LoadedGame = false;
        SceneManager.LoadScene("OverWorld");
        if(GameObject.Find("Player(Clone)") != null)
            GameObject.Find("Player(Clone)").GetComponent<PlayerController>().enabled = true;
    }

    private void AddContentToSaveFile()
    {
        PlayerData file1Data = SaveSystem.LoadPlayer($"/{PlayerPrefs.GetString(file1.FileName.name)}.SL");
        PlayerData file2Data = SaveSystem.LoadPlayer($"/{PlayerPrefs.GetString(file2.FileName.name)}.SL");
        PlayerData file3Data = SaveSystem.LoadPlayer($"/{PlayerPrefs.GetString(file3.FileName.name)}.SL");
        if (file1Data != null)
            Debug.Log(file1Data.totalHearts);
    }

    #endregion
}
