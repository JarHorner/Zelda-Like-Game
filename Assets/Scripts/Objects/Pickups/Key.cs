using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    #region Variables
    private PlayerUI playerUI;
    private DungeonManager dungeonManager;
    [SerializeField] private int dungeonNum;
    [SerializeField] private int keyNum;
    #endregion

    #region Methods

    void Start() 
    {
        playerUI = FindObjectOfType<PlayerUI>();
        dungeonManager = FindObjectOfType<AllDungeonsManager>().GetDungeonManager(dungeonNum);
        //if key has already been grabbed before, destroys object so it cant be re-collected.
        if(dungeonManager.GetKeyStayDestroyed(keyNum))
        {
            Destroy(gameObject);
        }
    }

    //enables grabbing a key and adding to dungeon currently in.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player" && !dungeonManager.GetKeyStayDestroyed(keyNum)) 
        {
            dungeonManager.AddKeyStayDestoryed(keyNum);
            dungeonManager.CurrentKeys += 1;
            playerUI.ChangeKeyCountText(dungeonNum);
            Destroy(gameObject);
        }
    }
    #endregion
}
