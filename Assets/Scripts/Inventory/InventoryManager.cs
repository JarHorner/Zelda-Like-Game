using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Variables
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    public PlayerInventory usableItems;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject assignButtonPopup;
    private GameObject itemBox1;
    private GameObject itemBox2;
    public List<InventorySlot> myInventorySlots = new List<InventorySlot>();
    public InventoryItem currentItem;
    private bool buttonChosen = false;
    private bool assignButtonMenuOpen = false;
    #endregion

    #region Methods

    //Sets the text and description to certain item. Used in InventorySlot when clicking on button.
    public void SetTextAndButton(string description, string name, bool buttonActive, InventoryItem newItem, GameObject itemText)
    {
        if (itemText)
        {
            itemText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
            itemText.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = description;
        }
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
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
                    Debug.Log("setting up item");
                    myInventorySlots[i].Setup(playerInventory.myInventory[i], this);
                    break;
                }
            }
        }
    }

    //when use button is pressed, this activates the popup that allocates an assigned button.
    public void UseButtonPressed() 
    {
        if (currentItem)
        {
            assignButtonPopup.SetActive(true);
            assignButtonMenuOpen = true;
        }
    }

    //helps assign an item to a button, ensuring 2 of the same item cannot be in the item boxes. once item is assigned, popup is deactivated.
    public void AssignToButton()
    {
        if (Input.GetButtonDown("UseItem1"))
        {
            itemBox1.GetComponent<Image>().enabled = true;
            itemBox1.GetComponent<Image>().sprite = currentItem.itemImage;
            usableItems.myInventory[0] = currentItem; 
            buttonChosen = true;

            if (usableItems.myInventory[1] == usableItems.myInventory[0])
            {
                itemBox2.GetComponent<Image>().sprite = null;
                itemBox2.GetComponent<Image>().enabled = false;
                usableItems.myInventory[1] = null;
            }
        }
        else if (Input.GetButtonDown("UseItem2"))
        {
            itemBox2.GetComponent<Image>().enabled = true;
            itemBox2.GetComponent<Image>().sprite = currentItem.itemImage;
            usableItems.myInventory[1] = currentItem;
            buttonChosen = true;

            if (usableItems.myInventory[0] == usableItems.myInventory[1])
            {
                itemBox1.GetComponent<Image>().sprite = null;
                itemBox1.GetComponent<Image>().enabled = false;
                usableItems.myInventory[0] = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonChosen = false;
            assignButtonPopup.SetActive(false);
            assignButtonMenuOpen = false;
        }
        if (buttonChosen)
        {
            buttonChosen = false;
            assignButtonPopup.SetActive(false);
            assignButtonMenuOpen = false;
        }
    }

    public void SwitchScreen(GameObject screenToOpen)
    {
        screenToOpen.SetActive(true);
    }

    public void CloseScreen(GameObject screenToClose)
    {
        screenToClose.SetActive(false);
    }

    private void OnApplicationQuit() 
    {
        usableItems.Reset();    
    }

    //gets the item boxes and populates some items in inventory. populated items will be removed eventually.
    void Start() 
    {
        itemBox1 = GameObject.Find("ItemBox1").transform.GetChild(0).gameObject;
        itemBox2 = GameObject.Find("ItemBox2").transform.GetChild(0).gameObject;
        PopulateInventorySlot("Heal");
        PopulateInventorySlot("Bow");
        PopulateInventorySlot("PowerGloves");
        PopulateInventorySlot("SwimmingMedal");
        PopulateInventorySlot("Sword");
        PopulateInventorySlot("Shield");
    }

    public bool AssignButtonMenuOpen
    {
        get { return assignButtonMenuOpen; }
    }

    public GameObject AssignButtonPopup
    {
        get { return assignButtonPopup; }
    }

    #endregion
}
