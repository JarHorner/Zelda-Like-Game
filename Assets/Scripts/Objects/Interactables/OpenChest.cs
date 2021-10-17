using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour 
{
    #region Variables
    private UIManager uIManager;
    private AllDungeonsManager allDungeonsManager;
    private GameManager gameManager;
    private Animator chestAnim;
    [SerializeField] private int chestNum;
    [SerializeField] private int dungeonNum;
    private bool canOpenChest = false;
    [SerializeField] private GameObject item;
    [SerializeField] private AudioSource audioSource;

    #endregion
    
    #region Methods

    void Start() 
    {
        uIManager = FindObjectOfType<UIManager>();
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
        gameManager = FindObjectOfType<GameManager>();
        chestAnim = GetComponent<Animator>();
        if(allDungeonsManager.GetDungeonManager(dungeonNum).GetChestStayOpen(chestNum))
        {
            chestAnim.SetBool("isOpened", true);
        }
    }

    void Update() 
    {
        if (canOpenChest && !allDungeonsManager.GetDungeonManager(dungeonNum).GetChestStayOpen(chestNum))
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
        audioSource.Play();
        item.GetComponent<SpriteRenderer>().enabled = true;
        item.transform.localPosition = new Vector2(0f, 0.5f);
        GetItem();
        yield return new WaitForSeconds(1.5f);
        Destroy(item);
        gameManager.UnPause();
        allDungeonsManager.GetDungeonManager(dungeonNum).AddChestStayOpen(chestNum);
    }

    private void GetItem()
    {
        SpriteRenderer item = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        string itemSpriteName = item.sprite.name;
        if (itemSpriteName.Contains("Money"))
        {
            int index = itemSpriteName.IndexOf('_');
            int moneyAmt = int.Parse(itemSpriteName.Substring(index + 1));
            Debug.Log(moneyAmt);
            uIManager.AddMoney(moneyAmt);
        }
        else if (itemSpriteName.Contains("Key"))
        {
            uIManager.AddKey(dungeonNum);
        }
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
