using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOpenDoor : MonoBehaviour
{
    #region variables
    private GameManager gameManager;
    private Animator doorAnimator;
    [SerializeField] AudioSource pressDown;
    [SerializeField] AudioSource doorAudioSource;
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;

    #endregion

    #region Unity Methods

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        doorAnimator = this.transform.parent.gameObject.GetComponentInChildren<Animator>();
    }
    
    //if movable block if over switch, door will open
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "MovableBlock")
        {
            pressDown.Play();
            StartCoroutine(OpenDoor());
        }
    }

    //if moveable block if off the switch, door will close
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "MovableBlock")
        {
            StartCoroutine(CloseDoor());
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenDoor() {
        doorAudioSource.clip = doorOpen;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", true);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door closing.
    IEnumerator CloseDoor() {
        doorAudioSource.clip = doorClose;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", false);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
    }
    #endregion
}
