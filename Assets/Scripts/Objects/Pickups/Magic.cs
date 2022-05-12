using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    #region Varibles

    #endregion

    #region Methods

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            MagicVisual.magicSystemStatic.Recover(2);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
