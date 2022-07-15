using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GettingItem : MonoBehaviour
{
    #region Variables
    private bool itemGained = false;
    [SerializeField] InventoryItem item;
    #endregion

    #region Methods

    public void PickupItem()
    {
        if (!itemGained)
        {
            FindObjectOfType<InventoryManager>().PopulateInventorySlot(item.itemName);
        }
    }

    #endregion
}
