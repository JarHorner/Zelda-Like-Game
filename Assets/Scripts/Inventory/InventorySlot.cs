using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    #region Variables
    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberInventoryText;
    [SerializeField] private Image itemImage;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;
    #endregion

    #region Methods

    private void OnEnable() 
    {
        if (thisItem != null && !thisItem.unique)
        {
            int numHeld; 
            int.TryParse(itemNumberInventoryText.text, out numHeld);
            Debug.Log(numHeld);
            if (thisItem.numberHeld != numHeld)
            {
                itemNumberInventoryText.text = "" + thisItem.numberHeld;
            }
        }
    }
    
    //puts item in inventory (sprite, number held, etc.) and becomes clickable.
    public void Setup(InventoryItem item, InventoryManager manager)
    {
        if (thisItem != null)
        {
            int numHeld = thisItem.numberHeld;
            itemNumberInventoryText.text = "" + numHeld;
        }
        else
        {
            thisItem = item;
            thisManager = manager;
            if (thisItem)
            {
                itemImage.GetComponent<Image>().enabled = true;
                itemImage.sprite = thisItem.itemImage;
                thisItem.playerOwns = true;
                if (!thisItem.unique)
                {
                    int numHeld = thisItem.numberHeld;
                    itemNumberInventoryText.text = "" + numHeld;
                }
                else
                {
                    itemNumberInventoryText.text = "";
                }
            }
        }
    }

    //when clicked on, shows the name and description of item.
    public void ShowDescription(GameObject itemText)
    {
        if (thisItem)
        {
            thisManager.SetTextAndButton(thisItem.itemDescription, thisItem.itemName, thisItem, itemText);
        }
    }

    #endregion
}
