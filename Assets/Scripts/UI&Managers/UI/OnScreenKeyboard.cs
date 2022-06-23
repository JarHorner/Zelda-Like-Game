using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using System.Text.RegularExpressions;

public class OnScreenKeyboard : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject controllerKeyboard;
    [SerializeField] private GameObject enteredFileName;
    [SerializeField] private GameObject[] keys;
    private TMP_Text selectedFileName;
    private TMP_InputField enteredFileNameText;
    private bool isCaps = true;

    #endregion

    #region Methods

    public void EnterFileName(TMP_Text selectedFileName)
    {
        this.gameObject.SetActive(true);
        this.selectedFileName = selectedFileName;
        enteredFileNameText = enteredFileName.GetComponent<TMP_InputField>();
        if (ControlScheme.IsController)
        {
            controllerKeyboard.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(GameObject.Find("Q").gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(enteredFileName);
        }
    }
    
    private void Update() 
    {
        if (!ControlScheme.IsController)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame && enteredFileNameText.text.Length > 0)
            {
                CreateFile();
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(enteredFileName); 
            }
        }
    }

    private void OnGUI() 
    {
        if (!ControlScheme.IsController)
        {
            char chr = Event.current.character;
            enteredFileNameText.text = Regex.Replace(enteredFileNameText.text, @"[^a-zA-Z0-9 ]", "");
        }
    }

    public void InputLetter(TMP_Text letter)
    {
        enteredFileNameText.text += letter.text;
    }

    public void Backspace()
    {
        enteredFileNameText.text = enteredFileNameText.text.Substring(0, enteredFileNameText.text.Length - 1);
    }

    public void ChangeCaps()
    {
        if (isCaps)
            isCaps = false;
        else
            isCaps = true;

        foreach (GameObject key in keys)
        {
            string keyText = key.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text;
            Debug.Log("before: " + keyText);
            if (isCaps)
                key.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = keyText.ToUpper();
            else
                key.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = keyText.ToLower();
            Debug.Log("after: " + key.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
        }
    }

    public void Enter()
    {
        if (enteredFileNameText.text.Length > 0)
        {
            CreateFile();
        }
    }

    private void CreateFile()
    {
        selectedFileName.text = enteredFileNameText.text;
        SaveSystem.CurrentFileName = $"/{enteredFileNameText.text}.SL";
        Debug.Log(SaveSystem.CurrentFileName);
        PlayerPrefs.SetString(selectedFileName.gameObject.name, enteredFileNameText.text);
        this.gameObject.SetActive(false);
        enteredFileNameText.text = "";
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedFileName.gameObject.transform.parent.gameObject);
    }

    public void LimitCharacter(TMP_InputField inputText)
    {
        if (inputText.text.Length > 8)
        {
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
        }
    }

    #endregion
}
