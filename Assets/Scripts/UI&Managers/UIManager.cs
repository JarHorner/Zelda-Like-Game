using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    #region Variables
        [SerializeField] Image dungeon0Key;
        private HealthManager healthManager;
        [SerializeField] private Slider healthBar;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text keyCount;
        [SerializeField] private TMP_Text moneyCount;
        private List<MutableKeyValPair<string, int>> dungeonkeys = new List<MutableKeyValPair<string, int>>();
        private List<MutableKeyValPair<string, bool>> dungeonEntranceKeys = new List<MutableKeyValPair<string, bool>>();
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject inventoryScreen;
    #endregion

    #region Unity Methods

    void Awake() 
    {
        //creates a keyvaluepair list to store amount of keys for each dungeon
        for (int i = 0; i < 8; i++)
        {
            dungeonkeys.Add(new MutableKeyValPair<string, int>("Dungeon" + i, 0));
        }
        //creates a keyvaluepair list to store the keys to open each dungeon
        for (int i = 0; i < 8; i++)
        {
            dungeonEntranceKeys.Add(new MutableKeyValPair<string, bool>($"Dungeon{i}Key", false));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        keyCount.text = "-";
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = healthManager.GetMaxHealth();
        healthBar.value = healthManager.GetCurrentHealth();
        //changes text of HP bar depending on previous calculations
        hpText.text = $"HP: {healthManager.GetCurrentHealth()}/{healthManager.GetMaxHealth()}";
    }

    //changes key count to the amt of keys you have in a specific dungeon
    public void ChangeKeyCount(int? dungeonNum = null)
    {
        if (dungeonNum == null) 
        {
            keyCount.text = "-";
        }
        else 
        {
            foreach(var item in dungeonkeys)
            {
                if (item.key.Contains(dungeonNum.ToString()))
                {
                    Debug.Log("Got Here!");
                    keyCount.text = item.value.ToString();
                }
            }
        }
    }

    //gets the key count of a certain dungeon
    public int GetKeyCount(string dungeonName)
    {
        foreach (var item in dungeonkeys)
        {
            if (item.key == dungeonName)
            {
                return item.value;
            }
        }
        return -1;
    }

    //adds key to certain dungeon
    public void AddKey(string dungeonName) 
    {
        foreach (var item in dungeonkeys)
        {
            if (item.key == dungeonName)
            {
                item.value += 1;
                keyCount.text = $"{item.value}";
            }
        }
    }

    //removes key from certain dungeon
    public void RemoveKey(string dungeonName)
    {
        foreach (var item in dungeonkeys)
        {
            if (item.key == dungeonName)
            {
                item.value -= 1;
                keyCount.text = $"{item.value}";
            }
        }
    }

    //gets the current amount of money
    public string GetMoneyCount()
    {
        return moneyCount.ToString();
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

    public GameObject GetPauseScreen()
    {
        return pauseScreen;
    }

    public GameObject GetInventoryScreen()
    {
        return inventoryScreen;
    }

    public void ActivateDungeonKey(string dungeonKeyName)
    {
        foreach (var item in dungeonEntranceKeys)
        {
            if (item.key == dungeonKeyName)
            {
                item.value = true;
                dungeon0Key.gameObject.SetActive(true);
            }
        }
    }

    public bool IsDungeonKeyActive(string dungeonKeyName)
    {
        foreach (var key in dungeonEntranceKeys)
        {
            if (key.key == dungeonKeyName && key.value == true)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}
