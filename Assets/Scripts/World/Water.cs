using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Water : MonoBehaviour
{
    private InventoryManager inventoryManager;
    [SerializeField] TilemapCollider2D waterCollider;
    
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager.HasSwimmingMedal())
        {
            waterCollider.isTrigger = true;
        }
    }

    
    void Update()
    {
        
    }
}
