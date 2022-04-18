using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    #region Variables
    private Animator playerAnim;
    private AreaTransitions areaTransition;
    private PlayerController player;
    private float animLength = 0.8f;
    private bool falling = false;
    [SerializeField] private FloatValue damageDealt;

    #endregion

    #region Methods

    void Start() 
    {
        player = FindObjectOfType<PlayerController>();
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        areaTransition = FindObjectOfType<AreaTransitions>();
    }

    void Update() 
    {
        //if the player is falling animation plays and movement is stopped.
        if (falling)
        {
            animLength -= Time.deltaTime;
            player.Movement = Vector2.zero;
            //once animation is finished, player is placed in their last postion and damage is taken.
            if (animLength <= 0)
            {
                player.transform.position = player.LastPlayerLocation;
                playerAnim.SetBool("isFalling", false); 
                player.DamagePlayer(damageDealt.InitalValue);
                player.moveSpeed = 6f;
                animLength = 0.5f;
                falling = false;
            }
        }
    }

    //plays the players falling animation and sets the falling bool to true (check Update())
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "HitBox" && enabled)
        {
            playerAnim.SetBool("isFalling", true); 
            player.moveSpeed = 0f;
            falling = true;
        }
    }

    //same as OnTriggerEnter2D() but ensures the player cannot glitch over anything!
    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.tag == "HitBox" && enabled)
        {
            playerAnim.SetBool("isFalling", true);  
            player.moveSpeed = 0f;
            falling = true;
        }
    }

    #endregion
}
