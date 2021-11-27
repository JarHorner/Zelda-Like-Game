using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogLocationCanvas : MonoBehaviour
{
    #region Variables
    private static bool exists;

    [SerializeField] private TMP_Text locationText;

    #endregion

    #region Methods
    private void Awake() 
    {
        if (!exists)
        {
            Debug.Log("Dialog up");
            exists = true;
            //ensures same player object is not destoyed when loading new scences
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.Log("Remove copy dialog");
            Destroy (gameObject);
        }
    }

    public TMP_Text LocationText
    {
        get { return locationText; }
        set { locationText = value; }
    }

    #endregion

}
