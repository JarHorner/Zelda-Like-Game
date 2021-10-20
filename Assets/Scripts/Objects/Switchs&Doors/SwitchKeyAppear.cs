using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchKeyAppear : MonoBehaviour
{
    #region Variables
    private bool usedSwitch = false;
    [SerializeField] GameObject key;
    [SerializeField] AudioSource pressDown;

    #endregion

    #region Unity Variables

    //if block is moved over collider, switch has not been used before and key has not been picked up, key will appear.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "MovableBlock" && !usedSwitch && key != null)
        {
            pressDown.Play();
            //finds key, then enables the sprite renderer so it can be picked up
            Debug.Log("Found");
            key.GetComponent<CapsuleCollider2D>().enabled = true;
            key.GetComponent<SpriteRenderer>().enabled = true;
            //turns trigger on switch off
            usedSwitch = true;
        }
    }

    #endregion

}
