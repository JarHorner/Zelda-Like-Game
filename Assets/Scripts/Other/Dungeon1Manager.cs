using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Dungeon1Manager
{
    #region Variables
    private static bool isOpened = false;
    private static string dungeonName = "Dungeon0";
    private static List<MutableKeyValPair<int, bool>> keyDoors = new List<MutableKeyValPair<int, bool>>();
    private static List<MutableKeyValPair<int, bool>> chests = new List<MutableKeyValPair<int, bool>>();
    #endregion

    #region Unity Methods
    public static string getDungeonName()
    {
        return dungeonName;
    }
    //adds a new door to stay opened to list, used in OpenKeyDoor when player unlocks door
    public static void addDoorStayOpen(int doorNum)
    {
        keyDoors.Add(new MutableKeyValPair<int, bool>(doorNum, true));
    }
    //checks to see if doorNum is in list, if not, door will not be opened when scene loads
    public static bool getDoorStayOpen(int doorNum)
    {
        foreach (var item in keyDoors)
        {
            if (item.key == doorNum)
            {
                Debug.Log(item.value);
                return item.value;
            }
        }
        return false;
    }

    //adds a new chest to stay opened to list, used in OpenChest Update function when player unlocks door
    public static void addChestStayOpen(int chestNum)
    {
        keyDoors.Add(new MutableKeyValPair<int, bool>(chestNum, true));
    }

    //checks to see if chestNum is in list, if not, chest will not be opened when scene loads
    public static bool getChestStayOpen(int chestNum)
    {
        foreach (var item in keyDoors)
        {
            if (item.key == chestNum)
            {
                Debug.Log(item.value);
                return item.value;
            }
        }
        return false;
    }

    public static void OpenDungeon1()
    {
        isOpened = true;
    }

    public static bool isDungeon1Opened()
    {
        return isOpened;
    }
    #endregion
}
