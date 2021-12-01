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
    private bool paused = false;

    #endregion

    #region Methods
    private void Start() 
    {
        dialogCanvas = FindObjectOfType<DialogLocationCanvas>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() 
    {
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                gameManager.UnPause();
                dialogCanvas.DialogBox.gameObject.SetActive(false);
            }
        }

        if (canRead && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Pressed Q");
            dialogCanvas.DialogBox.gameObject.SetActive(true);
            TMP_Text dialogText = dialogCanvas.DialogBox.GetComponentInChildren<TMP_Text>();
            dialogText.text = text;
            gameManager.Pause(true);
            paused = true;
            canRead = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("in-front of sign");
        if (other.tag == "Player")
        {
            canRead = true;
        }
    }

        private void OnTriggerExit2D(Collider2D other) 
    {
        Debug.Log("in-front of sign");
        if (other.tag == "Player")
        {
            canRead = false;
        }
    }

    #endregion

}