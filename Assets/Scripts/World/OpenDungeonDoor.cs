using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDungeonDoor : MonoBehaviour
{
    #region Variables

    [SerializeField] private Animator statueAnimator;
    [SerializeField] private Animator entranceAnimator;
    private UIManager uiManager;
    private GameManager gameManager;

    #endregion

    #region Unity Methods

    void Start() 
    {  
        uiManager = GameObject.FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        //if dungeon has been opened, it stays opened
        if (uiManager.isDungeon1Opened()) {
            statueAnimator.SetBool("Opened", true);
            entranceAnimator.SetBool("Opened", true);
            statueAnimator.Play("Base Layer.Sink_Idle", 0, 1f);
            entranceAnimator.Play("Base Layer.Open_Idle", 0, 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        //if player has key, start coroutine of dungeon-opening animation
        if (collider.gameObject.tag == "Player" && uiManager.isDungeonKeyActive("Dungeon0")) 
        {
            StartCoroutine(OpenDungeon());
        }
    }
    IEnumerator OpenDungeon() {
        //pauses game mamager while animations play (so player cannot move)
        gameManager.Pause(false);
        //sets flag so dungeon stays open
        uiManager.OpenDungeon1();
        //starts the process of animations
        statueAnimator.SetBool("hasKey", true);
        entranceAnimator.SetBool("hasKey", true);
        yield return new WaitForSeconds(3f);
        entranceAnimator.SetBool("Opened", true);
        gameManager.UnPause();
    }


    #endregion
}
