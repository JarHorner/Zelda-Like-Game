using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    #region Variables
    [SerializeField] private  int moneyCount;
    private UIManager uiManager;
    #endregion

    #region Methods
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }
    
    //enables grabbing money and adding to total (UiManager)
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            uiManager.AddMoney(moneyCount);
            Destroy(gameObject);
        }
    }
    #endregion
}
