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
    [SerializeField] private float timeToBeat;
    private IEnumerator coroutine;

    #endregion

    #region Unity Methods

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    //if players steps on collider and switch hasnt been used, door will open.
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" & !usedSwitch)
        {
            pressDown.Play();
            usedSwitch = true;
            //done like this so coroutine can be stopped if player leaves room. (Coroutines cannot be stopped if not an actual object).
            coroutine = OpenDoor();
            StartCoroutine(coroutine);
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenDoor() {
        isRunning = true;
        doorAudioSource.clip = doorOpen;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", true);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
        //sets an internal timer for the opened door. CloseDoor() will be played, if not beaten.
        yield return new WaitForSeconds(timeToBeat);
        isRunning = false;
        StartCoroutine(CloseDoor());
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door closing.
    IEnumerator CloseDoor() {
        doorAudioSource.clip = doorClose;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", false);
        usedSwitch = false;
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
    }

    //stops the current Coroutine (OpenDoor()) closing the door and allowing the switch to be pressed again.
    public void ResetDoor(bool doorOpen)
    {
        StopCoroutine(coroutine);
        doorAnimator.SetBool("Open", doorOpen);
        usedSwitch = doorOpen;
    }

    //checks if timer is currenly running.
    public bool IsRunning
    {
        get { return isRunning; }
    }
    #endregion
}
