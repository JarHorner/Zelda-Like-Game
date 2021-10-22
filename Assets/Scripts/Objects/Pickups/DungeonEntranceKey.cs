using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntranceKey : MonoBehaviour
{
    #region Variables
    [SerializeField] private int dungeonNum;
    private AllDungeonsManager allDungeonsManager;

    #endregion

    #region Methods

    private void Start() 
    {
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
    }

    //if player, picks up the item allowing the opening of a dungeon and showing in inventory.
    //more into in AllDungeonsManager.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player") 
        {
            allDungeonsManager.ActivateDungeonEntranceKey(dungeonNum);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
