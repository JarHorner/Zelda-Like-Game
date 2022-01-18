using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDungeonDoor : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioSource openDungeonToon;
    [SerializeField] private Animator statueAnimator;
    [SerializeField] private Animator entranceAnimator;
    [SerializeField] private int dungeonNum;
    private SoundManager soundManager;
    private AllDungeonsManager allDungeonsManager;
    private PauseGame pauseGame;

    #endregion

    #region Unity Methods

    void Start() 
    {  
        allDungeonsManager = GameObject.FindObjectOfType<AllDungeonsManager>();
        pauseGame = FindObjectOfType<PauseGame>();
        soundManager = FindObjectOfType<SoundManager>();
        //if dungeon has been opened, it stays opened when transitioning into other scenes.
        if (allDungeonsManager.GetDungeonManager(dungeonNum).IsDungeonOpened) {
            statueAnimator.SetBool("Opened", true);
            entranceAnimator.SetBool("Opened", true);
            statueAnimator.Play("Base Layer.Sink_Idle", 0, 1f);
            entranceAnimator.Play("Base Layer.Open_Idle", 0, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        //if player has key, start coroutine of dungeon-opening animation
        if (collider.gameObject.tag == "Player" && allDungeonsManager.IsDungeonEntranceKeyActive(dungeonNum)) 
        {
            StartCoroutine(OpenDungeon());
        }
    }
    IEnumerator OpenDungeon() {
        //pauses game mamager while animations play (so player cannot move)
        pauseGame.Pause(false);
        openDungeonToon.Play();
        //sets flag so dungeon stays open
        allDungeonsManager.GetDungeonManager(dungeonNum).IsDungeonOpened = true;
        //starts the process of animations
        statueAnimator.SetBool("hasKey", true);
        entranceAnimator.SetBool("hasKey", true);
        yield return new WaitForSeconds(3.5f);
        entranceAnimator.SetBool("Opened", true);
        pauseGame.UnPause();
    }


    #endregion
}
