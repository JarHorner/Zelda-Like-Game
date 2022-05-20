using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    #region Variables
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Text masterVolumeText;
    [SerializeField] TMP_Text bgmVolumeText;
    [SerializeField] TMP_Text effectVolumeText;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider bgmVolumeSlider;
    [SerializeField] Slider effectVolumeSlider;
    private float previousVolume;

    #endregion

    #region Methods

    void Start() 
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        //make a new string list, and populate it with each resolution as a string.
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //add list of options to the dropsown (only takes list of strings)
        options.Reverse();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //makes game fullcreen
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //sets quality of game to unity pre-sets
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //mutes the master volume, but also disabes the volume slider and changes the navigation so the slider cannot be used.
    public void SetMute(bool isMuted)
    {
        float volume;
        Navigation navigation;
        Button exitButton = GameObject.Find("Options_Exit_Button").GetComponent<Button>();
        Toggle mute = GameObject.Find("MuteToggle").GetComponent<Toggle>();
        if (isMuted)
        {
            audioMixer.GetFloat("MasterVolume", out previousVolume);
            volume = -80;
            masterVolumeSlider.interactable = false;
            bgmVolumeSlider.interactable = false;
            effectVolumeSlider.interactable = false;

            navigation = exitButton.navigation;
            navigation.selectOnUp = mute;
            exitButton.navigation = navigation;

            navigation = mute.navigation;
            navigation.selectOnDown = exitButton;
            mute.navigation = navigation;
        }
        else
        {
            volume = previousVolume;
            masterVolumeSlider.interactable = true;
            bgmVolumeSlider.interactable = true;
            effectVolumeSlider.interactable = true;

            navigation = exitButton.navigation;
            navigation.selectOnUp = effectVolumeSlider;
            exitButton.navigation = navigation;

            navigation = mute.navigation;
            navigation.selectOnDown = masterVolumeSlider;
            mute.navigation = navigation;
        }
        audioMixer.SetFloat("MasterVolume", volume);
    }

    //sets the master volume of the game. (ensure all different groups are affected)
    public void SetMasterVolume(float volume)
    {
        int text = Mathf.RoundToInt(volume * 1.25f) + 100;
        audioMixer.SetFloat("MasterVolume", volume);
        masterVolumeText.text = text + "%";
    }

    //sets the background volume of the game. (ensure all different groups are affected)
    public void SetBackgroundVolume(float volume)
    {
        int text = Mathf.RoundToInt(volume * 1.25f) + 100;
        audioMixer.SetFloat("BackgroundVolume", volume);
        bgmVolumeText.text = text + "%";
    }

    //sets the sound effect volume of the game. (ensure all different groups are affected)
    public void SetSoundEffectVolume(float volume)
    {
        int text = Mathf.RoundToInt(volume * 1.25f) + 100;
        audioMixer.SetFloat("SoundEffectVolume", volume);
        effectVolumeText.text = text + "%";
    }




    #endregion
}
