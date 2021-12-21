using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic structure of inventory, contains all items in a list.
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myInventory = new List<InventoryItem>();
}
