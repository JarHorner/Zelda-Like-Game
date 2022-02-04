using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Varibles

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            HealthVisual.healthSystemStatic.Heal(4);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
