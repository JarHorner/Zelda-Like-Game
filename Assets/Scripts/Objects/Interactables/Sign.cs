using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Sign : MonoBehaviour
{
    #region Variables
    [SerializeField] InputActionAsset inputMaster;
    private InputAction interact;
    [SerializeField] private string text;
    private DialogLocationCanvas dialogCanvas;
    private bool canRead = false;
    private PauseGame pauseGame;
    private PlayerController player;
    private bool paused = false;

    #endregion

    #region Methods
    private void Start() 
    {
        dialogCanvas = FindObjectOfType<DialogLocationCanvas>();
        pauseGame = FindObjectOfType<PauseGame>();
        player = FindObjectOfType<PlayerController>();
        var playerActionMap = inputMaster.FindActionMap("Player");

        interact = playerActionMap.FindAction("Interact");
    }

    private void Update() 
    {

    }

    private void ReadSign(InputAction.CallbackContext context)
    {
        if (paused)
        {
            pauseGame.UnPause();
            dialogCanvas.DialogBox.gameObject.SetActive(false);
            player.currentState = PlayerState.walk;
        }

        if (canRead)
        {
            dialogCanvas.DialogBox.gameObject.SetActive(true);
            player.currentState = PlayerState.interact;
            TMP_Text dialogText = dialogCanvas.DialogBox.GetComponentInChildren<TMP_Text>();
            dialogText.text = text;
            pauseGame.Pause(false);
            paused = true;
            canRead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "InteractBox")
        {
            canRead = true;
            if (!interact.enabled)
            {
                interact.performed += ReadSign;
                interact.Enable();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "InteractBox")
        {
            canRead = false;
            interact.performed -= ReadSign;
            interact.Disable();
        }
    }

    #endregion

}
