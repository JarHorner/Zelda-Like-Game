using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    #region Variables
    [SerializeField] private string text;
    private DialogLocationCanvas dialogCanvas;
    private bool canRead = false;
    private GameManager gameManager;
    private PlayerController player;
    private bool paused = false;

    #endregion

    #region Methods
    private void Start() 
    {
        dialogCanvas = FindObjectOfType<DialogLocationCanvas>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Update() 
    {
        if (paused)
        {
            if (Input.GetButtonDown("Interact"))
            {
                gameManager.UnPause();
                dialogCanvas.DialogBox.gameObject.SetActive(false);
                player.currentState = PlayerState.walk;
            }
        }

        if (canRead && Input.GetButtonDown("Interact"))
        {
            dialogCanvas.DialogBox.gameObject.SetActive(true);
            player.currentState = PlayerState.interact;
            TMP_Text dialogText = dialogCanvas.DialogBox.GetComponentInChildren<TMP_Text>();
            dialogText.text = text;
            gameManager.Pause(false);
            paused = true;
            canRead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "InteractBox")
        {
            canRead = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "InteractBox")
        {
            canRead = false;
        }
    }

    #endregion

}
