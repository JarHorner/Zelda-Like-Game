using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBomb : MonoBehaviour
{
    #region Variables

    #endregion

    #region  Methods

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<InventoryManager>().PopulateInventorySlot("StunBomb");
            Destroy(gameObject);
        }
    }

    #endregion
}
