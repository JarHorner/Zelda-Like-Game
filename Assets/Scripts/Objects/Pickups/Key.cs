using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    #region Variables
    private UIManager uiManager;
    private AllDungeonsManager allDungeonsManager;
    [SerializeField] private int dungeonNum;
    [SerializeField] private int keyNum;
    #endregion

    #region Methods

    void Start() 
    {
        uiManager = FindObjectOfType<UIManager>();
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
        if(allDungeonsManager.GetDungeonManager(dungeonNum).GetKeyStayDestroyed(keyNum))
        {
            Destroy(gameObject);
        }
    }

    //enables grabbing a key and adding to that dungeons place in UIManagers list.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player" && !allDungeonsManager.GetDungeonManager(dungeonNum).GetKeyStayDestroyed(keyNum)) 
        {
            //will need to change with more dungeons
            uiManager.AddKey(dungeonNum);
            allDungeonsManager.GetDungeonManager(dungeonNum).AddKeyStayDestoryed(keyNum);
            Destroy(gameObject);
        }
    }
    #endregion
}
