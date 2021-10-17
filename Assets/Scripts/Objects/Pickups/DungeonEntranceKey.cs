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
    public int getDungeonNum()
    {
        return dungeonNum;
    }

    private void Start() 
    {
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player") 
        {
            allDungeonsManager.ActivateDungeonKey(dungeonNum);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
