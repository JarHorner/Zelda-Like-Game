using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDoor : MonoBehaviour
{
    #region Variables
    private GameManager gameManager;
    [SerializeField] private AudioSource openDoor;
    [SerializeField] private Animator animator;
    private bool doorOpen = false;

    #endregion

    #region Methods 

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (doorOpen)
        {
            StartCoroutine(OpenEnemyDoor());
        }
    }
    

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenEnemyDoor()
    {
        openDoor.Play();
        gameManager.Pause(false);
        animator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        doorOpen = false;
        gameManager.UnPause();
    }

    public bool DoorOpen
    {
        get { return doorOpen; }
        set { doorOpen = value; }
    }

    #endregion
}
