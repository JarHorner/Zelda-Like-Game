using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyDoor : MonoBehaviour
{
    #region Variables
    private PlayerUI playerUI;
    private AllDungeonsManager allDungeonsManager;
    private PauseGame pauseGame;
    [SerializeField] private Animator animator;
    [SerializeField] private int dungeonNum;
    [SerializeField] private int doorNum;
    [SerializeField] private AudioSource openDoor;
    #endregion

    #region Unity Methods
    void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
        pauseGame = FindObjectOfType<PauseGame>();
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
        //if door has been opened, it will stay opened after leaving dungeon
        if(allDungeonsManager.GetDungeonManager(dungeonNum).GetDoorStayOpen(doorNum))
        {
            animator.SetBool("StaysOpened", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if player is against door, has a key and door is not in the list of doors to stay open, coroutine starts
        if(other.tag == "Player" && allDungeonsManager.GetDungeonManager(dungeonNum).CurrentKeys > 0 && !allDungeonsManager.GetDungeonManager(dungeonNum).GetDoorStayOpen(doorNum))
        {
            StartCoroutine(OpenDoor());
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenDoor() 
    {
        openDoor.Play();
        pauseGame.Pause(false);
        animator.SetBool("Open", true);
        allDungeonsManager.GetDungeonManager(dungeonNum).CurrentKeys -= 1;
        playerUI.ChangeKeyCountText(dungeonNum);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        pauseGame.UnPause();
        //adds chest to list so it cannot be opened again.
        allDungeonsManager.GetDungeonManager(dungeonNum).AddDoorStayOpen(doorNum);
    }
    #endregion
}
