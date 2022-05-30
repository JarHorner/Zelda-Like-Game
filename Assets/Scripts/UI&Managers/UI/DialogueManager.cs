using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Variables
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image speakerImage;
    [SerializeField] private Animator animator;
    private PauseGame pauseGame;
    private PlayerController player;
    private static DialogueManager _instance;
    private Queue<string> sentences;
    private bool startedConversation;

    #endregion
    void Awake() 
    {
        //Singleton Effect
        if (_instance != null && _instance != this)
        {
            Debug.Log($"Destroyed {this.gameObject}");
            Destroy (this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start() 
    {
        sentences = new Queue<string>();
        pauseGame = FindObjectOfType<PauseGame>();
        player = FindObjectOfType<PlayerController>();
    }

    //when player interacts with object that has dialog. Sets up the dialog that object has, then uses the DisplayNextSentence() 
    public void StartDialogue(Dialogue dialogue)
    {
        if(!startedConversation)
        {
            player.currentState = PlayerState.interact;
            pauseGame.Pause(false);
            nameText.text = dialogue.name;
            speakerImage.sprite = dialogue.sprite;
            animator.SetBool("IsOpen", true);
            
            //ensures queue is empty of past sentences, then populates queue with new sentences.
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            startedConversation = true;
        }

        DisplayNextSentence();
    }

    //Ends dialog if queue has no more sentences. Dequeue the sentence if the queue has another item.
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //types the sentence out 1 letter at a time in the text box.
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }

    //un-pauses the game and closes the text menus.
    private void EndDialogue()
    {
        player.currentState = PlayerState.walk;
        pauseGame.UnPause();
        animator.SetBool("IsOpen", false);
        startedConversation = false;
    }
    #region Methods

    #endregion
}
