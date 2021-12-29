using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlatformAppear : MonoBehaviour
{
   #region Variables
    private bool usedSwitch = false;
    [SerializeField] GameObject platform;
    [SerializeField] AudioSource pressDown;

    #endregion

    #region Variables

    //if block is moved over collider and switch has not been used before, platform will appear.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "MovableBlock" && !usedSwitch)
        {
            pressDown.Play();
            platform.transform.GetChild(0).gameObject.SetActive(true);
            foreach(Transform child in platform.transform.GetChild(1))
            {
                child.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            //turns trigger on switch off
            usedSwitch = true;
        }
    }
    #endregion
}