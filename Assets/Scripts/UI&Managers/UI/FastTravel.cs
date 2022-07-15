using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FastTravel : MonoBehaviour
{
    #region Varibles

    [SerializeField] private PauseGame pauseGame;
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction cancel;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject firstLocation;

    #endregion

    #region Methods

    public void OpenMenu()
    {
        panel.SetActive(true);

        pauseGame.Pause(false);

        var uiActionMap = inputMaster.FindActionMap("UI");

        cancel = uiActionMap.FindAction("Cancel");
        cancel.performed += ExitLocationMenu;
        cancel.Enable();

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(firstLocation);
    }

    public void FastTravelToLocation(FastTravelLocation location)
    {
        FindObjectOfType<PlayerController>().gameObject.transform.position = location.coordinates;
        FindObjectOfType<CameraController>().MinPosition = location.cameraMinBounds;
        FindObjectOfType<CameraController>().MaxPosition = location.cameraMaxBounds;

        ManaBar manaBar = FindObjectOfType<ManaBar>();
        manaBar.Mana.SpendMana(20);

        ExitLocationMenu();
        //animation
        //wait
        //ports
        //animation
        //wait
        //set camera
    }

    private void ExitLocationMenu(InputAction.CallbackContext context) 
    {
        cancel.performed -= ExitLocationMenu;
        cancel.Disable();

        panel.SetActive(false);
        pauseGame.UnPause();
    }

    private void ExitLocationMenu() 
    {
        cancel.performed -= ExitLocationMenu;
        cancel.Disable();

        panel.SetActive(false);
        pauseGame.UnPause();
    }

    #endregion
}
