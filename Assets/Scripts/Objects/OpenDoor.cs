using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    #region Variables

    public Animator animator;
    private UIManager uiManager;

    #endregion

    #region Unity Methods

    void Start() 
    {  
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player"&& int.Parse(uiManager.keyCount.text) > 0) 
        {
            animator.SetBool("hasKey", true);
        }
    }

    #endregion
}
