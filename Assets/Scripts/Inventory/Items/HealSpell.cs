using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : MonoBehaviour
{
    #region Variables
    private HealthManager healthManager;
    #endregion

    #region Methods

    public void IncreaseHealth(int amountIncrease)
    {
        healthManager = FindObjectOfType<HealthManager>();
        healthManager.Heal(amountIncrease);
    }

    #endregion
}
