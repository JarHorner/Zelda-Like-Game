using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    #region Variables
    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;
    #endregion

    #region Methods
    //puts item in inventory (sprite, number held, etc.) and becomes clickable.
    public void Setup(InventoryItem item, InventoryManager manager)
    {
        if (thisItem != null)
        {
            int numHeld = int.Parse(itemNumberText.text);
            itemNumberText.text = "" + numHeld;
        }
        else
        {
            thisItem = item;
            thisManager = manager;
            if (thisItem)
            {
                itemImage.GetComponent<Image>().enabled = true;
                itemImage.sprite = thisItem.itemImage;
                if (!thisItem.unique)
                {
                    int numHeld = thisItem.numberHeld;
                    itemNumberText.text = "" + numHeld;
                }
                else
                {
                    itemNumberText.text = "";
                }
            }
        }
    }

    //when clicked on, shows the name and description of item.
    public void ShowDescriptionAndButton(GameObject itemText)
    {
        if (thisItem)
        {
            thisManager.SetTextAndButton(thisItem.itemDescription, thisItem.itemName, thisItem.usable, thisItem, itemText);
        }
    }

    #endregion
}
