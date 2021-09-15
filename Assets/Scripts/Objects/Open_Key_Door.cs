using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Key_Door : MonoBehaviour
{
    #region Variables
    private UIManager uIManager;
    private GameManager gameManager;
    [SerializeField] private Animator animator;

    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && uIManager.getKeyCount() > 0)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor() {
        //pauses game while animations play (so player cannot move)
        gameManager.Pause(false);
        animator.SetBool("Open", true);
        uIManager.removeKey();
        yield return new WaitForSeconds(1.2f);
        gameManager.UnPause();
    }
    #endregion
}
