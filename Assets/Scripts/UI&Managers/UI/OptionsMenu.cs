using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    #region Variables
    private bool muted = false;
    private float currentVol;

    #endregion

    #region Methods

    public void MuteSound(GameObject confirmation)
    {
        if (!muted)
        {
            muted = true;
            confirmation.SetActive(true);
            currentVol = AudioListener.volume;
            AudioListener.volume = 0f;
        }
        else
        {
            muted = false;
            confirmation.SetActive(false);
            AudioListener.volume = currentVol;
        }
    }

    public void AlterMasterVolume(Slider slider)
    {
        AudioListener.volume = slider.value;
    }

    public void ChangeAttackButton(GameObject confirmation)
    {
        
    }


    #endregion
}
