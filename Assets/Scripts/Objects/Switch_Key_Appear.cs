using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Key_Appear : MonoBehaviour
{
    #region Variables
    private bool usedSwitch = false;

    #endregion

    #region Unity Variables
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "MovableBlock" && !usedSwitch)
        {
            //finds key, then enables the sprite renderer so it can be picked up
            if (GameObject.Find("Dungeon_Key"))
            {
                GameObject key = GameObject.Find("Dungeon_Key");
                key.GetComponent<CapsuleCollider2D>().enabled = true;
                key.GetComponent<SpriteRenderer>().enabled = true;
                //turns trigger on switch off
                usedSwitch = true;
            }
        }
    }

    #endregion

}
