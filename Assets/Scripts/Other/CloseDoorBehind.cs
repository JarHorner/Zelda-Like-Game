using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorBehind : MonoBehaviour
{
    #region Varibles
    private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource closeDoor;
    #endregion

    #region Methods
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if player is against door, has a key and door is not in the list of doors to stay open, coroutine starts
        if(other.tag == "Player")
        {
            StartCoroutine(CloseDoor());
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator CloseDoor() 
    {
        closeDoor.Play();
        gameManager.Pause(false);
        animator.SetBool("Open", false);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
    }

    #endregion
}
