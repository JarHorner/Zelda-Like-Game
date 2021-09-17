using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    #region Variables
        //variable ensures when changing scenes dungeon 1 is still open
        private bool dungeon1Opened = false;
        [SerializeField] Image dungeon1Key;
        private HealthManager healthManager;
        [SerializeField] private Slider healthBar;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text keyCount;
        private List<MutableKeyValPair<string, int>> keys = new List<MutableKeyValPair<string, int>>();
        private List<MutableKeyValPair<string, bool>> dungeonKeys = new List<MutableKeyValPair<string, bool>>();
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject inventoryScreen;
    #endregion

    #region Unity Methods

    void Awake() 
    {
        //creates a keyvaluepair list to store amount of keys for each dungeon
        for (int i = 0; i < 8; i++)
        {
            keys.Add(new MutableKeyValPair<string, int>("Dungeon" + i, 0));
        }
        //creates a keyvaluepair list to store the keys to open each dungeon
        for (int i = 0; i < 8; i++)
        {
            dungeonKeys.Add(new MutableKeyValPair<string, bool>("Dungeon" + i, false));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        Debug.Log("Called!");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = healthManager.getMaxHealth();
        healthBar.value = healthManager.getCurrentHealth();
        //changes text of HP bar depending on previous calculations
        hpText.text = $"HP: {healthManager.getCurrentHealth()}/{healthManager.getMaxHealth()}";
    }

    public void changeKeyCount(int? dungeonNum = null)
    {
        if (dungeonNum == null) 
        {
            keyCount.text = "-";
        }
        else 
        {
            foreach(var item in keys)
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
    public int getKeyCount(string dungeonName)
    {
        foreach (var item in keys)
        {
            if (item.key == dungeonName)
            {
                return item.value;
            }
        }
        return -1;
    }

    public void addKey(string dungeonName) 
    {
        foreach (var item in keys)
        {
            if (item.key == dungeonName)
            {
                item.value += 1;
                keyCount.text = $"{item.value}";
            }
        }
    }
    public void removeKey(string dungeonName)
    {
        foreach (var item in keys)
        {
            if (item.key == dungeonName)
            {
                item.value -= 1;
                keyCount.text = $"{item.value}";
            }
        }
    }

    public void OpenDungeon1()
    {
        dungeon1Opened = true;
    }

    public bool isDungeon1Opened()
    {
        return dungeon1Opened;
    }

    public GameObject getPauseScreen()
    {
        return pauseScreen;
    }

    public GameObject getInventoryScreen()
    {
        return inventoryScreen;
    }

    public void activateDungeonKey(int dungeonNum)
    {
        dungeonKeys[dungeonNum].value = true;
        dungeon1Key.gameObject.SetActive(true);
    }

    public bool isDungeonKeyActive(string dungeonName)
    {
        foreach (var key in dungeonKeys)
        {
            if (key.key == dungeonName && key.value == true)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
}
