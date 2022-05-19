using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    #region Variables

    public Dialogue dialogue;
    private ContextClue contextClue;
    [SerializeField] InputActionAsset inputMaster;
    private InputAction interact;

    #endregion

    #region Methods
    private void Start() 
    {
        contextClue = FindObjectOfType<ContextClue>();
        var playerActionMap = inputMaster.FindActionMap("Player");

        interact = playerActionMap.FindAction("Interact");
    }

    public void TriggerDialogue(InputAction.CallbackContext context)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "InteractBox")
        {
            contextClue.Interact();
            if (!interact.enabled)
            {
                interact.performed += TriggerDialogue;
                interact.Enable();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "InteractBox")
        {
            contextClue.Disappear();
            interact.performed -= TriggerDialogue;
            interact.Disable();
        }
    }

    #endregion
}
