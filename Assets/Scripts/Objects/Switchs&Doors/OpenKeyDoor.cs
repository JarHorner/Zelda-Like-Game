using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeyDoor : MonoBehaviour
{
    #region Variables
    private UIManager uIManager;
    private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private int doorNum;
    [SerializeField] private AudioSource openDoor;
    #endregion

    #region Unity Methods
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        //if door has been opened, it will stay opened after leaving dungeon
        if(Dungeon0Manager.getDoorStayOpen(doorNum))
        {
            animator.SetBool("StaysOpened", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if player is against door and has a key, coroutine starts
        if(other.tag == "Player" && uIManager.GetKeyCount(Dungeon0Manager.GetDungeonName()) > 0 && !Dungeon0Manager.getDoorStayOpen(doorNum))
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor() {
        //pauses game while animations play (so player cannot move)
        openDoor.Play();
        gameManager.Pause(false);
        animator.SetBool("Open", true);
        uIManager.RemoveKey(Dungeon0Manager.GetDungeonName());
        yield return new WaitForSeconds(1f);
        gameManager.UnPause();
        Dungeon0Manager.AddDoorStayOpen(doorNum);
    }
    #endregion
}
