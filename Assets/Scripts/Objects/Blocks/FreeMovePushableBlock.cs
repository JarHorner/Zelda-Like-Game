using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeMovePushableBlock : MonoBehaviour
{
    #region Variables
    [SerializeField] InputActionAsset inputMaster;
    private InputAction push, movement;
    private PlayerController player;
    private Animator playerAnim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource movingSound;
    private float startX, startY;
    private bool playSound = false;
    private bool notMoved = true;
    #endregion

    #region Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        playerAnim = player.GetComponent<Animator>();
        //sets the starting positions
        startX = transform.position.x;
        startY = transform.position.y;

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
            if (movement.ReadValue<Vector2>().y != 0)
            {
                PushBlock(true);
            }
            else if (movement.ReadValue<Vector2>().x != 0)
            {
                PushBlock(false);
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
            if (!movingSound.isPlaying)
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

    //moves the block in the direction params.
    private void PushBlock(bool direction)
    {
        playerAnim.SetBool("isPushing", true);
        notMoved = false;
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
    }

    //block can be pushed when players collider is within blocks.
    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            push.Enable();
        }
    }

    //gets the players animations back to regular movement and resets the time needed to push the block.
    private void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            push.Disable();
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