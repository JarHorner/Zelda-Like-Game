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
    public List<InventorySlot> myInventorySlots = new List<InventorySlot>();
    #endregion

    #region Methods

    public void SetText(string description, string name)
    {
        descriptionText.text = description;
        nameText.text = name;
    }

    public void PopulateInventorySlot(string itemName)
    {
        if (playerInventory)
        {
            Debug.Log("got here 1");
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                Debug.Log("got here 2");
                if (playerInventory.myInventory[i].ToString().Contains(itemName))
                {
                    Debug.Log("setting up bow");
                    myInventorySlots[i].Setup(playerInventory.myInventory[i], this);
                }
            }
        }
        Debug.Log("finished");
    }
    void Start() 
    {
        PopulateInventorySlot("Bow");
        SetText("", "");
    }

    #endregion
}
