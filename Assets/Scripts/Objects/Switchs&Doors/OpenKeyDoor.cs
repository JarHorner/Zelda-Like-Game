using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyDoor : MonoBehaviour
{
    #region Variables
    private UIManager uIManager;
    private AllDungeonsManager allDungeonsManager;
    private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private int dungeonNum;
    [SerializeField] private int doorNum;
    [SerializeField] private AudioSource openDoor;
    #endregion

    #region Unity Methods
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
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
        if(other.tag == "Player" && uIManager.GetKeyCount(dungeonNum) > 0 && !allDungeonsManager.GetDungeonManager(dungeonNum).GetDoorStayOpen(doorNum))
        {
            StartCoroutine(OpenDoor());
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenDoor() {
        openDoor.Play();
        gameManager.Pause(false);
        animator.SetBool("Open", true);
        uIManager.RemoveKey(dungeonNum);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
        //adds chest to list so it cannot be opened again.
        allDungeonsManager.GetDungeonManager(dungeonNum).AddDoorStayOpen(doorNum);
    }
    #endregion
}
