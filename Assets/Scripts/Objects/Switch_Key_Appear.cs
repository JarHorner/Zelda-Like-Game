using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Key_Appear : MonoBehaviour
{
    #region Variables
    private bool usedSwitch = false;
    [SerializeField] string keyName;
    [SerializeField] AudioSource pressDown;

    #endregion

    #region Unity Variables
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "MovableBlock" && !usedSwitch)
        {
            pressDown.Play();
            //finds key, then enables the sprite renderer so it can be picked up
            if (GameObject.Find(keyName))
            {
                GameObject key = GameObject.Find(keyName);
                key.GetComponent<CapsuleCollider2D>().enabled = true;
                key.GetComponent<SpriteRenderer>().enabled = true;
                //turns trigger on switch off
                usedSwitch = true;
            }
        }
    }

    #endregion

}
