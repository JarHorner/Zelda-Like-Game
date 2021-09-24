using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    #region Variables

    private PlayerController player;
    private Animator playerAnimator;

    #endregion
    
    #region Unity Methods

    void Start() {
        player = FindObjectOfType<PlayerController>();
        playerAnimator = player.GetComponent<Animator>();
    }

    //enables swimming in the water
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player") 
        {
            //sets swimming animation and new player speed
            playerAnimator.SetBool("isSwimming", true);
            player.setPlayerSwimming(true);
            player.setPlayerWalking(false);
            player.moveSpeed = 4f;
        }
    
    }

    #endregion
}
