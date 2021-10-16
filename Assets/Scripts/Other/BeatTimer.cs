using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTimer : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            doorAnimator.SetBool("StayOpen", true);
        }
    }
}
