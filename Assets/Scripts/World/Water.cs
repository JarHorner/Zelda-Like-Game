using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    #region Variables

    private PlayerController player;
    private Animator animator;

    #endregion
    
    #region Unity Methods

    void Start() {
        player = FindObjectOfType<PlayerController>();
        animator = player.GetComponent<Animator>();
    }
    //enables swimming in the water
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player") 
        {
            Debug.Log("Swimming Active");
            animator.SetBool("isSwimming", true);
            player.moveSpeed = 4f;
        }
    
    }

    #endregion
}
