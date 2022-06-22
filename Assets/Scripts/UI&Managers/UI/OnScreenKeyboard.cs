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
    private TMP_Text selectedFileName;
    private TMP_InputField enteredFileNameText;

    #endregion

    #region Methods

    public void EnterFileName(TMP_Text selectedFileName)
    {
        this.gameObject.SetActive(true);
        this.selectedFileName = selectedFileName;
        enteredFileNameText = enteredFileName.GetComponent<TMP_InputField>();
        enteredFileNameText.characterLimit = 10;
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
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                string newFileName = enteredFileName.GetComponent<TMP_InputField>().text;
                selectedFileName.text = newFileName;
                SaveSystem.CurrentFileName = $"/{newFileName}.SL";
                Debug.Log(SaveSystem.CurrentFileName);
                PlayerPrefs.SetString(selectedFileName.gameObject.name, newFileName);
                this.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(selectedFileName.gameObject.transform.parent.gameObject);
            }
        }
    }

    private void OnGUI() 
    {
        char chr = Event.current.character;
        enteredFileNameText.text = Regex.Replace(enteredFileNameText.text, @"[^a-zA-Z0-9 ]", "");
    }

    public void InputLetter(string letter)
    {
        enteredFileNameText.text += letter;
    }

    public void Backspace()
    {
        enteredFileNameText.text = enteredFileNameText.text.Substring(0, enteredFileNameText.text.Length - 1);
    }

    public void ChangeCaps()
    {
        
    }

    public void Enter()
    {
        selectedFileName.text = enteredFileNameText.text;
        SaveSystem.CurrentFileName = $"/{enteredFileNameText.text}.SL";
        Debug.Log(SaveSystem.CurrentFileName);
        PlayerPrefs.SetString(selectedFileName.gameObject.name, enteredFileNameText.text);
        this.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(selectedFileName.gameObject.transform.parent.gameObject);
    }

    #endregion
}
