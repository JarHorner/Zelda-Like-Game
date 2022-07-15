using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpell : MonoBehaviour
{
    #region Variables
    private float cost = 20;
    #endregion

    #region Methods

    public void OpenFastTravelMenu()
    {
        ManaBar manaBar = FindObjectOfType<ManaBar>();
        if (manaBar.Mana.CanSpend(cost))
        {
            FindObjectOfType<FastTravel>().OpenMenu(); 
        }
    }

    #endregion
}
