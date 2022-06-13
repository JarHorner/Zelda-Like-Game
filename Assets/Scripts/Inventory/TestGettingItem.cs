using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGettingItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "InteractBox")
        {
            Debug.Log("Getting Lanturn");
            FindObjectOfType<InventoryManager>().PopulateInventorySlot("Lanturn");
        }
    }
}
