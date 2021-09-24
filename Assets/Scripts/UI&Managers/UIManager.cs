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
    public int getKeyCount(string dungeonName)
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

    public void addKey(string dungeonName) 
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
    public void removeKey(string dungeonName)
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
    public GameObject getPauseScreen()
    {
        return pauseScreen;
    }

    public GameObject getInventoryScreen()
    {
        return inventoryScreen;
    }

    public void activateDungeonKey(string dungeonKeyName)
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

    public bool isDungeonKeyActive(string dungeonKeyName)
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
