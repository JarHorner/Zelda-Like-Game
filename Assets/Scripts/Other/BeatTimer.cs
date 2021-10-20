using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTimer : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator;

    //sets the doors animator bool to have the door stay opened for good.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            doorAnimator.SetBool("StayOpen", true);
        }
    }
}
