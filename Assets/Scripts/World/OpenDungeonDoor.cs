using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDungeonDoor : MonoBehaviour
{
    #region Variables

    public Animator statueAnimator;
    public Animator entranceAnimator;
    private UIManager uiManager;
    private GameManager gameManager;

    #endregion

    #region Unity Methods

    void Start() 
    {  
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        //if dungeon has been opened, it stays opened
        if (uiManager.dungeonOpened) {
            statueAnimator.SetBool("Opened", true);
            entranceAnimator.SetBool("Opened", true);
            statueAnimator.Play("Base Layer.Sink_Idle", 0, 1f);
            entranceAnimator.Play("Base Layer.Open_Idle", 0, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        //if player has key, start coroutine of dungeon-opening animation
        if (collider.gameObject.tag == "Player" && int.Parse(uiManager.keyCount.text) > 0) 
        {
            StartCoroutine(OpenDungeon());
        }
    }
    IEnumerator OpenDungeon() {
        //pauses game mamager while animations play (so player cannot move)
        gameManager.Pause(false);
        //sets flag so dungeon stays open
        uiManager.dungeonOpened = true;
        //starts the process of animations
        statueAnimator.SetBool("hasKey", true);
        entranceAnimator.SetBool("hasKey", true);
        uiManager.removeKey();
        yield return new WaitForSeconds(3f);
        entranceAnimator.SetBool("Opened", true);
        gameManager.UnPause();
    }


    #endregion
}
