using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerUI : MonoBehaviour
{

    #region Variables
    private HealthManager healthManager;
    private AllDungeonsManager allDungeonsManager;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text keyCount;
    [SerializeField] private TMP_Text moneyCount;
    [SerializeField] private GameObject itemBox1;
    [SerializeField] private GameObject itemBox2;
    #endregion

    #region Unity Methods

    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
    }

    void Update()
    {
        healthBar.maxValue = healthManager.MaxHealth;
        healthBar.value = healthManager.CurrHealth;
        //changes text of HP bar depending on previous calculations
        hpText.text = $"HP: {healthManager.CurrHealth}/{healthManager.MaxHealth}";
    }

    //changes key count to the amt of keys you have in a specific dungeon
    public void ChangeKeyCountText(int dungeonNum)
    {
        if (dungeonNum == -1) 
        {
            keyCount.text = "-";
        }
        else 
        {
            Debug.Log(allDungeonsManager.GetDungeonManager(dungeonNum));
            int numOfKeys = allDungeonsManager.GetDungeonManager(dungeonNum).CurrentKeys;
            keyCount.text = numOfKeys.ToString();
        }
    }

    //gets the current amount of money
    public int GetMoneyCount()
    {
        return int.Parse(moneyCount.text);
    }

    //adds money to total
    public void AddMoney(int amt) 
    {
        string count = moneyCount.text;
        Debug.Log(count);
        int total = int.Parse(moneyCount.text) + amt;
        Debug.Log(total.ToString());
        moneyCount.text = total.ToString();
    }

    //remove money to total
    public void RemoveMoney(int amt)
    {
        string count = moneyCount.text;
        int total = int.Parse(moneyCount.text) - amt;
        moneyCount.text = total.ToString();
    }
    
    public GameObject ItemBox1
    {
        get { return itemBox1; }
    }
    public GameObject ItemBox2
    {
        get { return itemBox2; }
    }
    #endregion
}
