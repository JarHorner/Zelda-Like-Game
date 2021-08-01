using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    #region Variables

    public Animator statueAnimator;
    public Animator EntranceAnimator;
    private UIManager uiManager;

    #endregion

    #region Unity Methods

    void Start() 
    {  
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player"&& int.Parse(uiManager.keyCount.text) > 0) 
        {
            statueAnimator.SetBool("hasKey", true);
            EntranceAnimator.SetBool("hasKey", true);
            int keys = int.Parse(uiManager.keyCount.text) - 1;
            uiManager.keyCount.text = $"{keys}";
            //EntranceAnimator.SetBool("Opened", true);
            //Destroy(this.gameObject);
        }
    }

    #endregion
}
