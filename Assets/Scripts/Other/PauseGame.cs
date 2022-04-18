using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private SoundManager soundManager;
    private AudioSource bgMusic;
    private PlayerController player;

    //pauses the the timescale of the game if true. if false, only the player will be stopped (good for cutscene stuff)
    public void Pause(bool timePause) 
    {
        if (timePause)
        {
            Time.timeScale = 0;
        }
        soundManager = FindObjectOfType<SoundManager>();
        player = FindObjectOfType<PlayerController>();
        bgMusic = soundManager.GetComponentInChildren<AudioSource>();       
        bgMusic.volume = 0.05f;
        player.enabled = false;
        player.IsMoving = false;
        player.GetComponent<Animator>().SetFloat("Speed", 0);
        player.currentState = PlayerState.menu;
        player.GetComponent<Animator>().SetBool("inMenu", true);
    }

    //returns the changes during a Pause back to normal
    public void UnPause() 
    {
        Time.timeScale = 1;
        soundManager = FindObjectOfType<SoundManager>();
        player = FindObjectOfType<PlayerController>();
        bgMusic = soundManager.GetComponentInChildren<AudioSource>();
        bgMusic.volume = 0.2f;
        player.enabled = true;
        player.GetComponent<Animator>().SetBool("inMenu", false);
    }
}
