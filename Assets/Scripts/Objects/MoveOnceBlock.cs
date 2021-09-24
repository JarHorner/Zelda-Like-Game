using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnceBlock : MonoBehaviour
{
    //Manages movable blocks, primarily used for switches and puzzles
    //Diagonal movement should be disabled
    //Once movement on an axis has occurred, movement should be disabled on the other axis
    #region Variables
    private Animator playerAnim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource movingSound;
    private float startX, startY;
    private Vector2 newPosition;
    private float pushingTime = 0.2f;
    private bool playSound = false;
    private bool isMoving = false;
    private float blockLength = 1f; //1f on both axes. Distance to move one block-length 
    private bool canPush = false;
    private bool notMoved = true;
    #endregion

    #region Methods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        startX = transform.position.x;
        startY = transform.position.y;
    }
 
    void Update()
    {
        if (canPush && notMoved)
        {
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.C))
            {
                PushBlock(true, new Vector2(startX, startY + blockLength));
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.C))
            {
                PushBlock(true, new Vector2(startX, startY - blockLength));
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.C))
            {
                PushBlock(false, new Vector2(startX + blockLength, startY));
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.C))
            {
                PushBlock(false, new Vector2(startX - blockLength, startY));
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

    void FixedUpdate() 
    {
        if (isMoving)
        {
            if (rb.position != newPosition)
            {
                Vector3 position = Vector3.MoveTowards(rb.position, newPosition, 1f * Time.deltaTime);
                rb.MovePosition(position);
            } else
            {
                isMoving = false;
                rb.bodyType = RigidbodyType2D.Static;
                notMoved = false;
            }
        }
    }

    // private void OnCollisionEnter2D(Collision2D collider) 
    // {
    //     if (collider.gameObject.tag == "Object" || collider.gameObject.tag == "Walls")
    //     {
    //         Debug.Log("Collision");
    //         transform.position = new Vector3(startX, startY, 0);
    //         isMoving = false;
    //     }
    // }

    private void OnCollisionStay2D(Collision2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            canPush = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collider) 
    {
        if (collider.gameObject.tag == "Player")
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
