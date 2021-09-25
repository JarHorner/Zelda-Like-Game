using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    #region Variables
    [SerializeField] private Animator animator;
    private bool steppedOn = false;
    private float falling = 1.8f;

    #endregion

    #region Methods
    void Update() 
    {
        if (steppedOn)
        {
            falling -= Time.deltaTime;

            if (falling <= 0)
            {
                //animator.SetBool("SteppedOn", false);
                //this.gameObject.SetActive(false);
                falling = 1.8f;
                steppedOn = false;
                this.GetComponent<BoxCollider2D>().enabled = true;
                this.GetComponent<Pitfall>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            animator.SetBool("SteppedOn", true);
            this.GetComponent<BoxCollider2D>().enabled = false;
            steppedOn = true;
        }    
    }

    #endregion
}
