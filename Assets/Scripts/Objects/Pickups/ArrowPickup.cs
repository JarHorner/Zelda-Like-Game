using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    #region Variables

    [SerializeField] private InventoryItem bowInvItem;

    #endregion

    #region Methods

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            int total = bowInvItem.numberHeld += 10;
            if (total > bowInvItem.maxNumberHeld)
                bowInvItem.numberHeld = bowInvItem.maxNumberHeld;
            Destroy(this.gameObject);
        }
    }

    #endregion
}
