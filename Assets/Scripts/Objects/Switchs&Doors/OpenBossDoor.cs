using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBossDoor : MonoBehaviour
{
    #region Variables
    private UIManager uIManager;
    private AllDungeonsManager allDungeonsManager;
    private PauseGame pauseGame;
    [SerializeField] private Animator entranceDoorAnim;
    [SerializeField] private Animator exitDoorAnim;
    [SerializeField] private int dungeonNum;
    [SerializeField] private AudioSource openDoor;
    #endregion

    #region Methods
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        pauseGame = FindObjectOfType<PauseGame>();
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if player is against door, has a key and door is not in the list of doors to stay open, coroutine starts
        if(other.tag == "Player" && allDungeonsManager.GetDungeonManager(dungeonNum).HasBossKey)
        {
            StartCoroutine(OpenDoor());
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenDoor() 
    {
        openDoor.Play();
        pauseGame.Pause(false);
        entranceDoorAnim.SetBool("Open", true);
        exitDoorAnim.SetBool("Open", true);
        //after 1 second, everything returns to normal.
        yield return new WaitForSeconds(1f);
        pauseGame.UnPause();
    }
    #endregion
}
