using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set at the beginning of room ajacent to timed switch to stop the internal timer, resetting the room,
public class ResetTimedDoor : MonoBehaviour
{
    #region Variables
    [SerializeField] private SwitchTimedDoor TimedDoor;
    #endregion

    #region Methods

    //if player is in collider and timer is running, door is reset.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player" && TimedDoor.IsRunning == true)
        {
            TimedDoor.ResetDoor();
        }
    }
    #endregion
}
