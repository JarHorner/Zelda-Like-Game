using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    #region Variables
    private Animator playerAnim;
    private AreaTransitions areaTransition;
    private PlayerController player;
    private HealthManager healthManager;
    private float animLength = 0.8f;
    private bool falling = false;

    #endregion

    #region Methods

    void Start() 
    {
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
        areaTransition = FindObjectOfType<AreaTransitions>();
        player = FindObjectOfType<PlayerController>();
        healthManager = FindObjectOfType<HealthManager>();
    }

    void Update() 
    {
        if (falling)
        {
            animLength -= Time.deltaTime;
            player.Movement = Vector2.zero;
            if (animLength <= 0)
            {
                player.transform.position = areaTransition.LastPlayerLocation;
                playerAnim.SetBool("isFalling", false); 
                healthManager.HurtPlayer(1);
                player.moveSpeed = 6f;
                animLength = 0.5f;
                falling = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player" && enabled)
        {
            playerAnim.SetBool("isFalling", true);  
            player.moveSpeed = 0f;
            falling = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if (collider.tag == "Player" && enabled)
        {
            playerAnim.SetBool("isFalling", true);  
            player.moveSpeed = 0f;
            falling = true;
        }
    }

    #endregion
}
