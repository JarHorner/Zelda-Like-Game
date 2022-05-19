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

    public void StartDialogue(Dialogue dialogue)
    {
        if(!startedConversation)
        {
            player.currentState = PlayerState.interact;
            pauseGame.Pause(false);
            nameText.text = dialogue.name;
            speakerImage.sprite = dialogue.sprite;
            animator.SetBool("IsOpen", true);
            
            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            startedConversation = true;
        }

        DisplayNextSentence();
    }

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

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

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
