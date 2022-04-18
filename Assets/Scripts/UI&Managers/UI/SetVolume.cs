using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public float currVol;
    public Slider slider;
    public Toggle toggle;

    void Start() 
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        if (PlayerPrefs.GetInt("IsOn") == 1)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        currVol = Mathf.Log10(slider.value) * 20;
    }
    
    public void AdjustVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20); 
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void Mute(bool isOn)
    {
        if (isOn)
        {
            currVol = Mathf.Log10(slider.value) * 20;
            mixer.SetFloat("MusicVol", -80f);
            PlayerPrefs.SetInt("IsOn", 1);
        }
        else
        {
            mixer.SetFloat("MusicVol", currVol);
            PlayerPrefs.SetInt("IsOn", 0);
        }
    }
}
