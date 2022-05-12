using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    #region Variables
    #endregion

    #region Methods

    public void IncreaseHealth(int amountIncrease)
    {
        int count = HealthVisual.healthSystemStatic.HeartList.Count - 1;
        if (!MagicVisual.magicSystemStatic.IsEmpty() && HealthVisual.healthSystemStatic.HeartList[count].Fragments != 4)
        {
            HealthVisual.healthSystemStatic.Heal(amountIncrease);
            MagicVisual.magicSystemStatic.Use(amountIncrease/2);
        }
    }

    #endregion
}
