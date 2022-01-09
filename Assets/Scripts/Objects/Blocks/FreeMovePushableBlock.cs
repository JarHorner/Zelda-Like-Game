using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovePushableBlock : MonoBehaviour
{
    #region Variables
    private Animator playerAnim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource movingSound;
    private float startX, startY;
    private Vector3 newPosition;
    private float pushingTime = 0.2f;
    private bool playSound = false;
    private bool canPush = false;
    private bool notMoved = true;
    #endregion

    #region Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        //sets the starting positions
        startX = transform.position.x;
        startY = transform.position.y;
    }
 
    void Update()
    {
        //if player can push, PushBlock() will be called with a different param 
        //depending on the facing of the player.
        if (canPush)
        {
            if (Input.GetButton("Vertical") && Input.GetButton("Push"))
            {
                PushBlock(true);
            }
            else if (Input.GetButton("Horizontal") && Input.GetButton("Push"))
            {
                PushBlock(false);
            }
        }
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


    //moves the block in the direction params.
    private void PushBlock(bool direction)
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
            playSound = true;
        }
    }

    //block can be pushed when players collider is within blocks.
    private void OnCollisionStay2D(Collision2D collider) 
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            canPush = true;
        }
    }

    //gets the players animations back to regular movement and resets the time needed to push the block.
    private void OnCollisionExit2D(Collision2D collider) 
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            //sets the players animation back to idle/walking when not interacting with block
            playerAnim.SetBool("isPushing", false);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            canPush = false;
            pushingTime = 0.2f;
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