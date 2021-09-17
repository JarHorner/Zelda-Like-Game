using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody2D rb;
    private Animator playerAnim;
    [SerializeField] private AudioSource movingSound;
    private float y;
    private float x;
    private float pushingTime = 0.2f;

    #endregion

    #region Unity Methods
    void Start()
    {
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
    }

    void Update() 
    {
         if (pushingTime <= 0)
        {
            if (!movingSound.isPlaying)
            {
                movingSound.Play();
            }
        }
        else
        {
            movingSound.Stop();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //gets the current x and y position of the block
        y = rb.transform.position.y;
        x = rb.transform.position.x;
    }

    private void OnCollisionStay2D(Collision2D collider) {

        if (collider.gameObject.tag == "Player")
        {
            pushingTime -= Time.deltaTime;
            //sets players pushing animation
            playerAnim.SetBool("isPushing", true);
            //when the player has been pushing a certain amt of time, the block will move based on the direction the play is facing
            if (pushingTime <= 0)
            {
                //changes the constraints to only allow certain movement ex. if pushing right, wont ever slide upo or down.
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                if (y > rb.transform.position.y || y < rb.transform.position.y)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                } 
                else if (x > rb.transform.position.x || x < rb.transform.position.x)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collider) {
        if (collider.gameObject.tag == "Player")
        {
            //sets the players animation back to idle/walking when not interacting with block
            playerAnim.SetBool("isPushing", false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
            pushingTime = 0.2f;
        }
    }

    #endregion
}
