using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    #region Variables
    private UIManager uIManager;
    private AllDungeonsManager allDungeonsManager;
    [SerializeField] private int dungeonNum;
    [SerializeField] private int keyNum;
    #endregion

    #region Methods

    void Start() 
    {
        uIManager = FindObjectOfType<UIManager>();
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
        //if key has already been grabbed before, destroys object so it cant be re-collected.
        if(allDungeonsManager.GetDungeonManager(dungeonNum).GetKeyStayDestroyed(keyNum))
        {
            Destroy(gameObject);
        }
    }

    //enables grabbing a key and adding to dungeon currently in.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player" && !allDungeonsManager.GetDungeonManager(dungeonNum).GetKeyStayDestroyed(keyNum)) 
        {
            allDungeonsManager.GetDungeonManager(dungeonNum).AddKeyStayDestoryed(keyNum);
            allDungeonsManager.GetDungeonManager(dungeonNum).CurrentKeys += 1;
            uIManager.ChangeKeyCountText(dungeonNum);
            Destroy(gameObject);
        }
    }
    #endregion
}
