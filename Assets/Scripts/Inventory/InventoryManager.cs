using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    #region Variables    
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    public PlayerInventory usableItems;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject itemBox1;
    [SerializeField] private GameObject itemBox2;
    public List<InventorySlot> myInventorySlots = new List<InventorySlot>();
    public InventoryItem currentItem;
    #endregion

    #region Methods

    //gets the item boxes and populates some items in inventory. populated items will be removed eventually.
    void Awake() 
    {
        PopulateInventorySlot("Heal");
        PopulateInventorySlot("Bow");
        PopulateInventorySlot("Lanturn");
        PopulateInventorySlot("PowerGloves");
        PopulateInventorySlot("SwimmingMedal");
        PopulateInventorySlot("Sword");
        PopulateInventorySlot("Shield");
    }

    //Sets the text and description to certain item. Used in InventorySlot when clicking on button.
    public void SetTextAndButton(string description, string name, InventoryItem newItem, GameObject itemText)
    {
        if (itemText)
        {
            itemText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            itemText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
        }
        currentItem = newItem;
    }

    //used when picking up new item, ex. OpenChest.
    public void PopulateInventorySlot(string itemName)
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (playerInventory.myInventory[i] == null)
                {
                    //Move on, will delete later
                }
                else if (playerInventory.myInventory[i].ToString().Contains(itemName))
                {
                    myInventorySlots[i].Setup(playerInventory.myInventory[i], this);
                    break;
                }
            }
        }
    }

    public void AssignItem(InputAction.CallbackContext context)
    {
        AssignItemToSlot(context);
    }

    //helps assign an item to a button, ensuring 2 of the same item cannot be in the item boxes. once item is assigned, popup is deactivated.
    private void AssignItemToSlot(InputAction.CallbackContext context)
    {
        currentItem = EventSystem.current.currentSelectedGameObject.GetComponent<InventorySlot>().thisItem;
        if (context.action.name.Equals("AssignItem1") && currentItem.usable)
        {
            itemBox1.transform.GetChild(0).GetComponent<Image>().enabled = true;
            itemBox1.transform.GetChild(0).GetComponent<Image>().sprite = currentItem.itemImage;
            if (currentItem.numberHeld > -1)
                itemBox1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = currentItem.numberHeld.ToString();
            else
                itemBox1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            usableItems.myInventory[0] = currentItem; 

            if (usableItems.myInventory[1] == usableItems.myInventory[0])
            {
                itemBox2.transform.GetChild(0).GetComponent<Image>().sprite = null;
                itemBox2.transform.GetChild(0).GetComponent<Image>().enabled = false;
                itemBox2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                usableItems.myInventory[1] = null;
            }
        }
        else if (context.action.name.Equals("AssignItem2") && currentItem.usable)
        {
            itemBox2.transform.GetChild(0).GetComponent<Image>().enabled = true;
            itemBox2.transform.GetChild(0).GetComponent<Image>().sprite = currentItem.itemImage;
            if (currentItem.numberHeld > -1)
                itemBox2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = currentItem.numberHeld.ToString();
            else
                itemBox2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            usableItems.myInventory[1] = currentItem;

            if (usableItems.myInventory[0] == usableItems.myInventory[1])
            {
                itemBox1.transform.GetChild(0).GetComponent<Image>().sprite = null;
                itemBox1.transform.GetChild(0).GetComponent<Image>().enabled = false;
                itemBox1.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                usableItems.myInventory[0] = null;
            }
        }
    }

    //Used by buttons to switch inventory screens.
    public void SwitchScreen(GameObject screenToOpen)
    {
        screenToOpen.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set a new selected object
        EventSystem.current.SetSelectedGameObject(screenToOpen.transform.Find("ToOtherScreens").GetChild(0).gameObject);
    }

    //Used by buttons to switch inventory screens.
    public void CloseScreen(GameObject screenToClose)
    {
        screenToClose.SetActive(false);
    }

    private void OnApplicationQuit() 
    {
        usableItems.Reset();    
    }

    public bool HasLanturn()
    {
        foreach (var item in playerInventory.myInventory)
        {
            if (item != null && item.name == "Lanturn")
            {
                if (item.playerOwns == true)
                    return true;
            }
        }
        return false;
    }

    public bool HasSwimmingMedal()
    {
        foreach (var item in playerInventory.myInventory)
        {
            if (item != null && item.name == "SwimmingMedal")
            {
                if (item.playerOwns == true)
                    return true;
            }
        }
        return false;
    }

    #endregion
}
