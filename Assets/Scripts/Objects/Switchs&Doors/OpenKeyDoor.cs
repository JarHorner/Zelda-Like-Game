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
        //if player is against door and has a key, coroutine starts
        if(other.tag == "Player" && uIManager.GetKeyCount(dungeonNum) > 0 && !allDungeonsManager.GetDungeonManager(dungeonNum).GetDoorStayOpen(doorNum))
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor() {
        //pauses game while animations play (so player cannot move)
        openDoor.Play();
        gameManager.Pause(false);
        animator.SetBool("Open", true);
        uIManager.RemoveKey(dungeonNum);
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
        allDungeonsManager.GetDungeonManager(dungeonNum).AddDoorStayOpen(doorNum);
    }
    #endregion
}
