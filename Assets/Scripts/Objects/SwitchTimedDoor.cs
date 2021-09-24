using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTimedDoor : MonoBehaviour
{
    #region variables
    private GameManager gameManager;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource pressDown;
    [SerializeField] private AudioSource doorAudioSource;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;

    #endregion

    #region Unity Methods

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    //if door will open when player walks over switch
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            pressDown.Play();
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor() {
        //pauses game while animations play (so player cannot move)
        doorAudioSource.clip = doorOpen;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
        yield return new WaitForSeconds(15f);
        StartCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor() {
        //pauses game while animations play (so player cannot move)
        doorAudioSource.clip = doorClose;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", false);
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
    }
    #endregion
}
