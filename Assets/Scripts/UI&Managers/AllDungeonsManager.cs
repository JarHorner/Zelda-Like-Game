using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllDungeonsManager : MonoBehaviour
{
    #region Variables
    [SerializeField] Image dungeon0Key;
    [SerializeField] DungeonManager dungeonManager;
    private List<MutableKeyValPair<int, DungeonManager>> dungeons = new List<MutableKeyValPair<int, DungeonManager>>();
    private List<MutableKeyValPair<int, bool>> dungeonEntranceKeys = new List<MutableKeyValPair<int, bool>>();
    #endregion

    #region Methods

    void Awake() 
    {
        //creates a keyvaluepair list to store amount of dungeons. (Will be improved with more dungeons)
        for (int i = 0; i < 8; i++)
        {
            DungeonManager dungeon = new DungeonManager();
            AddDungeon(i, dungeon);
        }

        //creates a keyvaluepair list to store the entrance keys to open each dungeon
        for (int i = 0; i < 8; i++)
        {
            dungeonEntranceKeys.Add(new MutableKeyValPair<int, bool>(i, false));
        }
    }
    //adds dungeon to to list
    private void AddDungeon(int num, DungeonManager manager)
    {
        dungeons.Add(new MutableKeyValPair<int, DungeonManager>(num, manager));
    }

    //gets the DungeonManager object assigned to the dungeonNum key
    public DungeonManager GetDungeonManager(int num)
    {
        foreach (var item in dungeons)
        {
            if (item.key == num)
            {
                return item.value;
            }
        }
        return null;
    }

    public void ActivateDungeonKey(int dungeonNum)
    {
        foreach (var item in dungeonEntranceKeys)
        {
            if (item.key == dungeonNum)
            {
                item.value = true;
                dungeon0Key.gameObject.SetActive(true);
            }
        }
    }

    public bool IsDungeonKeyActive(int dungeonNum)
    {
        foreach (var key in dungeonEntranceKeys)
        {
            if (key.key == dungeonNum && key.value == true)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
