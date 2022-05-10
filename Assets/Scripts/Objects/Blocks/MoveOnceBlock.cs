using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveOnceBlock : MonoBehaviour
{
    //Manages movable blocks, primarily used for switches and puzzles
    //Diagonal movement should be disabled
    //Once movement on an axis has occurred, movement should be disabled on the other axis
    #region Variables
    [SerializeField] InputActionAsset inputMaster;
    private InputAction push, movement;
    private PlayerController player;
    private Animator playerAnim;
    private Rigidbody2D rb;
    private GameObject thisGameObject;
    [SerializeField] private AudioSource movingSound;
    private float startX, startY;
    private Vector2 newPosition;
    private bool playSound = false;
    private bool notMoved = true;
    private bool inFinalPosition = false;
    
    //values Lerp uses
    float lerpDuration = 0.5f;

    //these two varibles can be adjusted at any time
    private float blockLength = 1f; //1f on both axes. Distance to move one block-length
    #endregion

    #region Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        playerAnim = player.GetComponent<Animator>();
        //sets the starting positions
        //startX = transform.position.x;
        //startY = transform.position.y;

        var playerActionMap = inputMaster.FindActionMap("Player");

        push = playerActionMap.FindAction("Push");
        push.performed += OnPush;
        push.canceled += OnRelease;
        movement = playerActionMap.FindAction("Movement");
    }

    private void OnPush(InputAction.CallbackContext context)
    {
        if (push.interactions.Length == 4)
        {
            startX = thisGameObject.transform.position.x;
            startY = thisGameObject.transform.position.y;
            if (movement.ReadValue<Vector2>().y != 0)
            {
                if (movement.ReadValue<Vector2>().y == 1)
                {
                    newPosition = new Vector2(startX, startY + blockLength);
                    PushBlock(true, newPosition);
                }
                else
                {
                    newPosition = new Vector2(startX, startY - blockLength);
                    PushBlock(true, newPosition);
                }
            }
            else if (movement.ReadValue<Vector2>().x != 0)
            {
                if (movement.ReadValue<Vector2>().x == 1)
                {
                    newPosition = new Vector2(startX + blockLength, startY);
                    PushBlock(false, newPosition);
                }
                else
                {
                    newPosition = new Vector2(startX - blockLength, startY);
                    PushBlock(false, newPosition);
                }
            }
            playSound = true;
        }
    }

    private void OnRelease(InputAction.CallbackContext context)
    {
        playSound = false;
        playerAnim.SetBool("isPushing", false);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (playSound)
        {
            if (!movingSound.isPlaying && playSound)
            {
                movingSound.Play();
            }
        }
        else
        {
            movingSound.Stop();
            playSound = false;
        }
    }

    //moves the block in the direction and to the position params.
    private void PushBlock(bool direction, Vector2 position)
    {
        playerAnim.SetBool("isPushing", true);
        player.MovementAudioSource.Stop();
        //true is up or down, false is left or right
        if (direction)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        StartCoroutine(LerpBlock());
    }

    //movement happens here. smooth lerp.
    IEnumerator LerpBlock()
    {
        float timeElapsed = 0;
        Vector2 startPosition = thisGameObject.transform.position;
        while (timeElapsed < lerpDuration)
        {
            thisGameObject.transform.position = Vector3.Lerp(startPosition, newPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            Debug.Log(timeElapsed);
            yield return null;
        }
        thisGameObject.transform.position = newPosition;

        thisGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        inFinalPosition = true;
        notMoved = false;
    }

    //block can be pushed when players collider is within blocks.
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "InteractBox" && !inFinalPosition)
        {
            push.Enable();
            thisGameObject = this.gameObject;
        }
    }

    //gets the players animations back to regular movement and resets the time needed to push the block.
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            push.Disable();
            thisGameObject = null;
            playSound = false;
            playerAnim.SetBool("isPushing", false);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public bool NotMoved
    {
        set { notMoved = value; }
    }

    public float StartX
    {
        get { return startX; }
    }

    public float StartY
    {
        get { return startY; }
    }
    #endregion
}
