using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    #region Variables
    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject useButton;
    public List<InventorySlot> myInventorySlots = new List<InventorySlot>();
    public InventoryItem currentItem;
    #endregion

    #region Methods

    //Sets the text and description to certain item. Used in InventorySlot when clicking on button.
    public void SetTextAndButton(string description, string name, bool buttonActive, InventoryItem newItem)
    {
        descriptionText.text = description;
        nameText.text = name;
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

    public void UseButtonPressed() 
    {
        if (currentItem)
        {
            currentItem.Use();
        }
    }

    void Start() 
    {
        PopulateInventorySlot("Heal");
    }

    #endregion
}
