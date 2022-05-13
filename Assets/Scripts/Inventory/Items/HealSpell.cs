using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    #region Variables
    private float cost = 45;
    #endregion

    #region Methods

    public void IncreaseHealth(int amountIncrease)
    {
        int count = HealthVisual.healthSystemStatic.HeartList.Count - 1;
        ManaBar manaBar = FindObjectOfType<ManaBar>();
        if (HealthVisual.healthSystemStatic.HeartList[count].Fragments != 4 && manaBar.Mana.CanSpend(cost))
        {
            HealthVisual.healthSystemStatic.Heal(amountIncrease);
            manaBar.Mana.SpendMana(cost);
            Debug.Log("Spent");
        }
    }

    #endregion
}
