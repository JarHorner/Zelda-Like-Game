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
        HealthVisual.healthSystemStatic.Heal(amountIncrease);
    }

    #endregion
}
