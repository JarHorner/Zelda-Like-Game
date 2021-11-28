using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogLocationCanvas : MonoBehaviour
{
    #region Variables
    private static bool exists;
    [SerializeField] private TMP_Text locationText;
    [SerializeField] private GameObject dialogBox;

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

    public GameObject DialogBox
    {
        get { return dialogBox; }
    }

    #endregion

}
