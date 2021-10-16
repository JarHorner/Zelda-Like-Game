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
        if (canPush)
        {
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.C))
            {
                PushBlock(true);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.C))
            {
                PushBlock(true);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.C))
            {
                PushBlock(false);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.C))
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
    #endregion
}