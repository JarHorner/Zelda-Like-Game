using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    #region Variables
    private static bool objectExists;

    #endregion

    #region Methods

    void Awake() 
    {
        if (!objectExists)
        {
            objectExists = true;
            //ensures same player object is not destoyed when loading new scences
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
