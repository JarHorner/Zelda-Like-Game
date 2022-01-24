using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    #region Variables
    [SerializeField] private  int moneyCount;
    private PlayerUI playerUI;
    #endregion

    #region Methods
    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
    }
    
    //enables grabbing money and adding to total (UiManager)
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerUI.AddMoney(moneyCount);
            Destroy(gameObject);
        }
    }
    #endregion
}
