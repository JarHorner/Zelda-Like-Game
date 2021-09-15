using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Key_Appear : MonoBehaviour
{
    #region Variables
    private bool switchUsed = false;

    #endregion

    #region Unity Methods
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "MovableBlock" && !switchUsed)
        {
            //finds key, then enables the sprite renderer so it can be picked up
            GameObject key = GameObject.Find("Key");
            key.GetComponent<CapsuleCollider2D>().enabled = true;
            key.GetComponent<SpriteRenderer>().enabled = true;
            //turns trigger on switch off
            switchUsed = true;
        }
    }

    #endregion
}
