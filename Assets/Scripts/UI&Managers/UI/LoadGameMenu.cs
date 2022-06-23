using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGameMenu : MonoBehaviour
{
    #region  variables
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private OnScreenKeyboard onScreenKeyboard;
    [SerializeField] private TMP_Text deleteFileText;
    [SerializeField] private SaveFile file1;
    [SerializeField] private SaveFile file2;
    [SerializeField] private SaveFile file3;
    private bool startGame = false;
    private bool newFileMade = false;
    #endregion

    #region Mathods

    private void Start() 
    {
        file1.FileName.text = PlayerPrefs.GetString(file1.FileName.name);
        file2.FileName.text = PlayerPrefs.GetString(file2.FileName.name);
        file3.FileName.text = PlayerPrefs.GetString(file3.FileName.name);

        AddContentToSaveFile();
    }

    public void StartORLoadGame(TMP_Text saveFileName)
    {
        if (startGame)
        {
            StartGame();
            return;
        }
        
        newFileMade = false;
        if (mainMenu.OpeningNewFile && !newFileMade)
        {
            if (saveFileName.text != "")
            {
                return;
            }
            else
            {
                onScreenKeyboard.EnterFileName(saveFileName);
                newFileMade = true;
                startGame = true;
            }
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

    public void ChangeButtonToDeleteFile()
    {
        file1.gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
        file2.gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
        file3.gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();

        UnityEventTools.AddPersistentListener(file1.gameObject.GetComponent<Button>().onClick, delegate{DeleteFile(file1.FileName);});
        UnityEventTools.AddPersistentListener(file2.gameObject.GetComponent<Button>().onClick, delegate{DeleteFile(file2.FileName);});
        UnityEventTools.AddPersistentListener(file3.gameObject.GetComponent<Button>().onClick, delegate{DeleteFile(file3.FileName);});

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(file1.gameObject);

        deleteFileText.enabled = true;
    }

    void DeleteFile(TMP_Text saveFileName)
    {

        file1.gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
        file2.gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
        file3.gameObject.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();

        UnityEventTools.AddPersistentListener(file1.gameObject.GetComponent<Button>().onClick, delegate{StartORLoadGame(file1.FileName);});
        UnityEventTools.AddPersistentListener(file2.gameObject.GetComponent<Button>().onClick, delegate{StartORLoadGame(file2.FileName);});
        UnityEventTools.AddPersistentListener(file3.gameObject.GetComponent<Button>().onClick, delegate{StartORLoadGame(file3.FileName);});

        SaveSystem.DeletePlayer(saveFileName.text);
        AddContentToSaveFile();

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(file1.gameObject);

        deleteFileText.enabled = false;
    }

    //loads the first scene of the game
    private void StartGame() 
    {
        SaveSystem.LoadedGame = false;
        startGame = false;
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
            CheckCollectedItems(file1Data, file1);
        else
            RemoveCollectedItems(file1);
        
        if (file2Data != null)
            CheckCollectedItems(file2Data, file2);
        else
            RemoveCollectedItems(file2);

        if (file3Data != null)
            CheckCollectedItems(file3Data, file3);
        else
            RemoveCollectedItems(file3);
    }

    private void CheckCollectedItems(PlayerData playerData, SaveFile file)
    {
        for (int i = 0; i < playerData.totalHearts; i++)
        {
            file.Hearts[i].enabled = true;
        }
        if (playerData.hasLanturn)
            file.Lanturn.enabled = true;
        if (playerData.hasSwimmingMedal)
            file.SwimmingMedal.enabled = true;
    }

    private void RemoveCollectedItems(SaveFile file)
    {
        file.FileName.text = "";
        for (int i = 0; i < file.Hearts.Length; i++)
        {
            file.Hearts[i].enabled = false;
        }
        file.Lanturn.enabled = false;
        file.SwimmingMedal.enabled = false;
    }

    #endregion
}
