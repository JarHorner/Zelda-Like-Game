using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class Dungeon0Manager
{
    #region Variables
    private static bool isOpened = false;
    private static bool hasMap = false;
    private static string dungeonName = "Dungeon0";
    private static List<MutableKeyValPair<int, bool>> keyDoors = new List<MutableKeyValPair<int, bool>>();
    private static List<MutableKeyValPair<int, bool>> chests = new List<MutableKeyValPair<int, bool>>();
    private static List<MutableKeyValPair<int, bool>> keys = new List<MutableKeyValPair<int, bool>>();
    #endregion

    #region Unity Methods
    public static string GetDungeonName()
    {
        return dungeonName;
    }

    //adds a new door to stay opened to list, used in OpenKeyDoor when player unlocks door
    public static void AddDoorStayOpen(int doorNum)
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
    public static void AddChestStayOpen(int chestNum)
    {
        keyDoors.Add(new MutableKeyValPair<int, bool>(chestNum, true));
    }

    //checks to see if chestNum is in list, if not, chest will not be opened when scene loads
    public static bool GetChestStayOpen(int chestNum)
    {
        foreach (var item in keyDoors)
        {
            if (item.key == chestNum)
            {
                return item.value;
            }
        }
        return false;
    }

     //adds a new key to stay destroyed to list, used in Key OnTriggerEnter2D function when player unlocks door
    public static void AddKeyStayDestoryed(int keyNum)
    {
        keys.Add(new MutableKeyValPair<int, bool>(keyNum, true));
    }

    //checks to see if keyNum is in list, if not, key will not be destroyed on opening the scene.
    public static bool GetKeyStayDestroyed(int keyNum)
    {
        foreach (var item in keys)
        {
            if (item.key == keyNum)
            {
                return item.value;
            }
        }
        return false;
    }

    public static void OpenDungeon0()
    {
        isOpened = true;
    }

    public static bool IsDungeon0Opened()
    {
        return isOpened;
    }

    public static void GetMap()
    {
        hasMap = true;
    }

    public static bool HasMap()
    {
        return hasMap;
    }
    #endregion
}
