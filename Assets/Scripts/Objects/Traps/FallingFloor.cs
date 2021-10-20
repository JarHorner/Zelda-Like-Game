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
        //if the object is stepped on, the animation will play out.
        if (steppedOn)
        {
            falling -= Time.deltaTime;

            //once animation is over, the object turns into a pitfall.
            if (falling <= 0)
            {
                falling = 1.8f;
                steppedOn = false;
                this.GetComponent<BoxCollider2D>().enabled = true;
                this.GetComponent<Pitfall>().enabled = true;
            }
        }
    }

    //if player steps on collider, animation will begin, and the collider will be disabled to prevent interupting running animation.
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
