using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ControlMenu : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject keyboardControls;
    [SerializeField] private GameObject gamepadControls;
    [SerializeField] private TMP_Text currentControls;
    [SerializeField] private Button controlsMenuExitButton;
    [SerializeField] GameObject changeKeyboardControlsFirstButton;
    [SerializeField] GameObject changeGamepadControlsFirstButton;
    [SerializeField] GameObject optionsFirstButton;


    #endregion

    #region Methods

    //opens control change menu
    public void OpenChangeControlsMenu(GameObject controlPanel)
    {
        if (!ControlScheme.IsController)
        {
            KeyboardControls();
            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(changeKeyboardControlsFirstButton);
        }
        else
        {
            GamepadControls();
            //clear selected object
            EventSystem.current.SetSelectedGameObject(null);
            //set a new selected object
            EventSystem.current.SetSelectedGameObject(changeGamepadControlsFirstButton);
        }
        controlPanel.GetComponent<Animator>().SetBool("IsActive", true);
    }

    //closes control change menu
    public void CloseChangeControlsMenu(GameObject controlPanel)
    {
        controlPanel.GetComponent<Animator>().SetBool("IsActive", false);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void ChangeInputDeviceContolMenu()
    {
        Navigation navigation = controlsMenuExitButton.navigation;
        navigation.mode = Navigation.Mode.Explicit;

        if (keyboardControls.activeSelf)
            GamepadControls();
        else
            KeyboardControls();
    }

    private void KeyboardControls()
    {
        Navigation navigation = controlsMenuExitButton.navigation;
        navigation.mode = Navigation.Mode.Explicit;

        currentControls.text = "Keyboard";
        gamepadControls.SetActive(false);
        keyboardControls.SetActive(true);

        navigation.selectOnUp = keyboardControls.transform.GetChild(7).GetChild(1).gameObject.GetComponent<Button>();
        navigation.selectOnDown = keyboardControls.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
        controlsMenuExitButton.navigation = navigation;
    }

    private void GamepadControls()
    {
        Navigation navigation = controlsMenuExitButton.navigation;
        navigation.mode = Navigation.Mode.Explicit;

        currentControls.text = "Gamepad";
        keyboardControls.SetActive(false);
        gamepadControls.SetActive(true);

        navigation.selectOnUp = gamepadControls.transform.GetChild(7).GetChild(1).gameObject.GetComponent<Button>();
        navigation.selectOnDown = gamepadControls.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
        controlsMenuExitButton.navigation = navigation;
    }


    #endregion
}
