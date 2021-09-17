using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_Open_Door : MonoBehaviour
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

    IEnumerator OpenDoor() {
        //pauses game while animations play (so player cannot move)
        doorAudioSource.clip = doorOpen;
        doorAudioSource.Play();
        gameManager.Pause(false);
        doorAnimator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
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
