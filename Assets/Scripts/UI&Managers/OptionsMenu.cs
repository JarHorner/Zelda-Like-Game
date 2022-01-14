using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class OptionsMenu : MonoBehaviour
{

    #region Variables
    private bool muted = false;

    #endregion

    #region Methods

    public void MuteSound(GameObject confirmation)
    {
        if (!muted)
        {
            muted = true;
            confirmation.SetActive(true);
            AudioListener.volume = 0f;
        }
        else
        {
            muted = false;
            confirmation.SetActive(false);
            AudioListener.volume = 1f;
        }
    }

    public void ChangeAttackButton(GameObject confirmation)
    {
        
    }


    #endregion
}
