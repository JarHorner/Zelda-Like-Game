using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyDoor : MonoBehaviour
{
    #region Variables
    private UIManager uIManager;
    private DungeonManager dungeonManager;
    private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private int doorNum;
    private bool staysOpen = false;
    #endregion

    #region Unity Methods
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        dungeonManager = FindObjectOfType<DungeonManager>();
        gameManager = FindObjectOfType<GameManager>();
        //if door has been opened, it will stay opened after leaving dungeon
        if(dungeonManager.getDoorStayOpen(doorNum))
        {
            animator.SetBool("StaysOpened", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if player is against door and has a key, coroutine starts
        if(other.tag == "Player" && uIManager.getKeyCount(dungeonManager.getDungeonName()) > 0)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor() {
        //pauses game while animations play (so player cannot move)
        Debug.Log("Id" + doorNum);
        gameManager.Pause(false);
        animator.SetBool("Open", true);
        uIManager.removeKey(dungeonManager.getDungeonName());
        yield return new WaitForSeconds(1.2f);
        gameManager.UnPause();
        dungeonManager.addDoorStayOpen(doorNum);
    }
    #endregion
}
