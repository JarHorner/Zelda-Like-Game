using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class OnScreenKeyboard : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject controllerKeyboard;
    [SerializeField] private GameObject enteredFileName;
    private TMP_Text selectedFileName;

    #endregion

    #region Methods

    public void EnterFileName(TMP_Text selectedFileName)
    {
        this.gameObject.SetActive(true);
        this.selectedFileName = selectedFileName;
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

    #endregion
}
