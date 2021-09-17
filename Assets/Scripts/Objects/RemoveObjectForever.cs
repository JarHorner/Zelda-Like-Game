using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjectForever : MonoBehaviour
{
    #region Variables
    
    private static bool created = false;
    #endregion

    #region Unity Methods
    void Awake()
    {
        if (created)
        {
            Destroy(this.gameObject);
            created = true;
        }
        else {
            created = true;
        }
    }
    #endregion
}
