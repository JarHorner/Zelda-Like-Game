using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOpenDoor : MonoBehaviour
{
    #region variables
    private PauseGame pauseGame;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private Animator secondDoorAnimator;
    [SerializeField] AudioSource pressDown;
    [SerializeField] AudioSource doorAudioSource;
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioClip doorClose;

    #endregion

    #region Unity Methods

    void Start()
    {
        pauseGame = FindObjectOfType<PauseGame>();
        doorAnimator = this.transform.parent.gameObject.GetComponentInChildren<Animator>();
    }
    
    //if movable block if over switch, door will open
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "MovableBlock")
        {
            pressDown.Play();
            if (secondDoorAnimator != null)
            {
               secondDoorAnimator.SetBool("Open", true);
            }
            StartCoroutine(OpenDoor());
        }
    }

    //if moveable block if off the switch, door will close
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "MovableBlock")
        {
            if (transform.parent.gameObject.transform.childCount == 3)
            {
               Animator otherDoorAnimator = this.transform.parent.gameObject.transform.Find("Other_Switch_Door").GetComponent<Animator>();
               otherDoorAnimator.SetBool("Open", false);
            }
            StartCoroutine(CloseDoor());
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenDoor() {
        doorAudioSource.clip = doorOpen;
        doorAudioSource.Play();
        pauseGame.Pause(false);
        doorAnimator.SetBool("Open", true);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        pauseGame.UnPause();
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door closing.
    IEnumerator CloseDoor() {
        doorAudioSource.clip = doorClose;
        doorAudioSource.Play();
        pauseGame.Pause(false);
        doorAnimator.SetBool("Open", false);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        pauseGame.UnPause();
    }
    #endregion
}
