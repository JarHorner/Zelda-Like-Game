using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseGame : MonoBehaviour
{
    private PlayerController player;

    //pauses the the timescale of the game if true. if false, only the player will be stopped (good for cutscene stuff)
    public void Pause(bool timePause) 
    {
        if (timePause)
        {
            Time.timeScale = 0;
        }
        player = FindObjectOfType<PlayerController>();    
        player.enabled = false;
        player.GetComponent<Animator>().SetFloat("Speed", 0);
        player.GetComponent<Animator>().SetBool("inMenu", true);
    }

    //returns the changes during a Pause back to normal
    public void UnPause() 
    {
        Time.timeScale = 1;
        player = FindObjectOfType<PlayerController>();
        player.enabled = true;
        player.GetComponent<Animator>().SetBool("inMenu", false);
    }
}
