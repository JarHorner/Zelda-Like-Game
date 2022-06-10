using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerUI : MonoBehaviour
{

    #region Variables
    private AllDungeonsManager allDungeonsManager;
    [SerializeField] private InputActionAsset inputMaster;
    private InputActionMap playerActionMap;
    [SerializeField] private TMP_Text keyCount;
    [SerializeField] private TMP_Text moneyCount;
    [SerializeField] private GameObject itemBox1;
    [SerializeField] private TMP_Text itemBox1Control;
    [SerializeField] private GameObject itemBox2;
    [SerializeField] private TMP_Text itemBox2Control;
    private bool switched = true;
    #endregion

    #region Unity Methods

    void Start()
    {
        allDungeonsManager = FindObjectOfType<AllDungeonsManager>();
        playerActionMap = inputMaster.FindActionMap("Player");
        ChangeItemBoxButton();
        switched = ControlScheme.IsController;
    }

    private void Update() 
    {
        if (switched != ControlScheme.IsController)
        {
            ChangeItemBoxButton();
            switched = ControlScheme.IsController;
        }

    }

    private void ChangeItemBoxButton()
    {
        if (!ControlScheme.IsController)
        {
            itemBox1Control.text = playerActionMap.FindAction("UseItem1").GetBindingDisplayString(0);
            itemBox2Control.text = playerActionMap.FindAction("UseItem2").GetBindingDisplayString(0);
        }
        else
        {
            itemBox1Control.text = playerActionMap.FindAction("UseItem1").GetBindingDisplayString(1);
            itemBox2Control.text = playerActionMap.FindAction("UseItem2").GetBindingDisplayString(1);
        }
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
