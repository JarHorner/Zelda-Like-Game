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
    private Animator playerAnim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource movingSound;
    private float startX, startY;
    private Vector2 newPosition;
    private bool playSound = false;
    private bool isMoving = false;
    private bool canPush = false;
    private bool notMoved = true;

    //these two varibles can be adjusted at any time
    private float pushingTime = 0.2f;
    private float blockLength = 1f; //1f on both axes. Distance to move one block-length 
    #endregion

    #region Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        //sets the starting positions
        startX = transform.position.x;
        startY = transform.position.y;

        var playerActionMap = inputMaster.FindActionMap("Player");

        push = playerActionMap.FindAction("Push");
        movement = playerActionMap.FindAction("Movement");
    }
 
    void Update()
    {
        //if player can push and the block hasnt moved yet, PushBlock() will be called with a different param 
        //depending on the facing of the player.
        if (canPush && notMoved)
        {
            if (movement.ReadValue<Vector2>().y != 0 && push.ReadValue<float>() == 1)
            {
                if (movement.ReadValue<Vector2>().y == 1)
                {
                    PushBlock(true, new Vector2(startX, startY + blockLength));
                }
                else
                {
                    PushBlock(true, new Vector2(startX, startY - blockLength));
                }
            }
            else if (movement.ReadValue<Vector2>().x != 0 && push.ReadValue<float>() == 1)
            {
                if (movement.ReadValue<Vector2>().x == 1)
                {
                    PushBlock(false, new Vector2(startX + blockLength, startY));
                }
                else
                {
                    PushBlock(false, new Vector2(startX - blockLength, startY));
                }
            }
        }
        //if player is pushing and the sound is not currently being played, new sound will play, creating a loop.
        //will stop if not being pushed.
        if (pushingTime <= 0)
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
        pushingTime -= Time.deltaTime;
        if (pushingTime <= 0)
        {
            //true is up or down, false is left or right
            if (direction)
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            else
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            //rb.isKinematic = true;
            newPosition = position;
            isMoving = true;
            playSound = true;
        }
    }

    //movement happens here, ensures movement is not based on framerate.
    void FixedUpdate() 
    {
        if (isMoving)
        {
            if (rb.position != newPosition)
            {
                Vector3 position = Vector3.MoveTowards(rb.position, newPosition, 1f * Time.deltaTime);
                rb.MovePosition(position);
            } 
            else
            {
                isMoving = false;
                rb.bodyType = RigidbodyType2D.Static;
                notMoved = false;
            }
        }
    }

    //block can be pushed when players collider is within blocks.
    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            canPush = true;
            push.Enable();
        }
    }

    //gets the players animations back to regular movement and resets the time needed to push the block.
    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            //sets the players animation back to idle/walking when not interacting with block
            playerAnim.SetBool("isPushing", false);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            canPush = false;
            pushingTime = 0.2f;
            push.Disable();
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
