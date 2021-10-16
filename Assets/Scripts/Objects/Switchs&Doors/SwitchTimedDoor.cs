using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTimedDoor : MonoBehaviour
{
    #region variables
    private GameManager gameManager;
    private bool usedSwitch = false;
    private bool isRunning = false;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioSource pressDown;
    [SerializeField] private AudioSource doorAudioSource;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClose;
    private IEnumerator coroutine;

    #endregion

    #region Unity Methods

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    //if door will open when player walks over switch
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" & !usedSwitch)
        {
            pressDown.Play();
            usedSwitch = true;
            coroutine = OpenDoor();
            StartCoroutine(coroutine);
        }
    }

    IEnumerator OpenDoor() {
        //pauses game while animations play (so player cannot move)
        isRunning = true;
        doorAudioSource.clip = doorOpen;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
        yield return new WaitForSeconds(15f);
        isRunning = false;
        StartCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor() {
        //pauses game while animations play (so player cannot move)
        doorAudioSource.clip = doorClose;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", false);
        usedSwitch = false;
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
    }

    public void ResetDoor()
    {
        StopCoroutine(coroutine);
        doorAnimator.SetBool("Open", false);
        usedSwitch = false;
    }

    public bool IsRunning
    {
        get { return isRunning; }
    }
    #endregion
}
