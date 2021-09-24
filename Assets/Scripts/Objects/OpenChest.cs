using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour 
{
    #region Variables
    private Animator chestAnim;
    private bool canOpenChest = false;

    #endregion
    
    #region Methods

    void Start() 
    {
        chestAnim = GetComponent<Animator>();
    }

    void Update() 
    {
        if (canOpenChest)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canOpenChest = false;
                chestAnim.SetBool("isOpened", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D Collider) 
    {
        if (Collider.tag == "Player") {
            canOpenChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Collider) 
    {
        if (Collider.tag == "Player") {
            canOpenChest = false;
        }
    }

    #endregion
}
