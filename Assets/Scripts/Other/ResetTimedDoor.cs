using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimedDoor : MonoBehaviour
{
    #region Variables
    [SerializeField] private SwitchTimedDoor TimedDoor;
    #endregion

    #region Methods

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player" && TimedDoor.IsRunning == true)
        {
            TimedDoor.ResetDoor();
        }
    }
    #endregion
}
