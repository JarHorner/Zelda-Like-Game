using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    #region Variables
    private UIManager uiManager;
    [SerializeField] private int keyNum;
    #endregion

    #region Methods

    void Start() 
    {
        uiManager = FindObjectOfType<UIManager>();
        if(Dungeon0Manager.GetKeyStayDestroyed(keyNum))
        {
            Destroy(gameObject);
        }
    }

    //enables grabbing a key and adding to that dungeons place in UIManagers list.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player" && !Dungeon0Manager.GetKeyStayDestroyed(keyNum)) 
        {
            //will need to change with more dungeons
            uiManager.AddKey(Dungeon0Manager.GetDungeonName());
            Dungeon0Manager.AddKeyStayDestoryed(keyNum);
            Destroy(gameObject);
        }
    }
    #endregion
}
