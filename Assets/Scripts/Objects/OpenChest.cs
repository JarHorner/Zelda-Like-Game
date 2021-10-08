using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour 
{
    #region Variables
    private UIManager uIManager;
    private GameManager gameManager;
    private Animator chestAnim;
    [SerializeField] private int chestNum;
    private bool canOpenChest = false;
    [SerializeField] private GameObject item;

    #endregion
    
    #region Methods

    void Start() 
    {
        uIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        chestAnim = GetComponent<Animator>();
        if(Dungeon1Manager.getChestStayOpen(chestNum))
        {
            chestAnim.SetBool("isOpened", true);
        }
    }

    void Update() 
    {
        if (canOpenChest)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Open());
            }
        }
    }

    IEnumerator Open()
    {
        canOpenChest = false;
        chestAnim.SetBool("isOpened", true);
        gameManager.Pause(false);
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.transform.localPosition = new Vector2(0f, 0.5f);
        yield return new WaitForSeconds(1.5f);
        Destroy(item);
        gameManager.UnPause();
        Dungeon1Manager.addChestStayOpen(chestNum);
    }

    private void OnTriggerEnter2D(Collider2D Collider) 
    {
        if (Collider.tag == "Player") {
            canOpenChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D Collider) 
    {
        if (Collider.tag == "Player") {
            canOpenChest = false;
        }
    }

    #endregion
}
