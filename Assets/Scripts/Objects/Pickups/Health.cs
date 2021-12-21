using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Varibles
    private HealthManager healthManager;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            if (healthManager.CurrHealth != healthManager.MaxHealth)
            {
                healthManager.Heal(1);
            }
            Destroy(this.gameObject);
        }
    }
    #endregion
}
