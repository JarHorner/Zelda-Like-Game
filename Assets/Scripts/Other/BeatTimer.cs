using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTimer : MonoBehaviour
{

    [SerializeField] private Animator doorAnimator;
    [SerializeField] private SwitchTimedDoor timedDoor;

    //sets the doors animator bool to have the door stay opened for good.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            doorAnimator.SetBool("StayOpen", true);
            if (timedDoor.IsRunning)
            {
                timedDoor.ResetDoor(true);
            }
        }
    }
}
