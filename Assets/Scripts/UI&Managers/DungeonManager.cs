using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonManager : MonoBehaviour
{
    #region Variables
    private static bool firstTimeInDungeon = true;
    [SerializeField] private string dungeonName;
    private List<MutableKeyValPair<int, bool>> keyDoors = new List<MutableKeyValPair<int, bool>>();
    #endregion

    #region Unity Methods

    void Awake() 
    {
        //if the player enters the dungeon for the first time, 
        //a keyvaluepair list is made and the class will not be destoryed upon leaving scene
        if (firstTimeInDungeon)
        {
            DontDestroyOnLoad(this.gameObject);
            firstTimeInDungeon = false;
            keyDoors = new List<MutableKeyValPair<int, bool>>();
        }
    }

    public string getDungeonName()
    {
        return dungeonName;
    }
    //adds a new door to stay opened to list, used in OpenKeyDoor when player unlocks door
    public void addDoorStayOpen(int doorNum)
    {
        keyDoors.Add(new MutableKeyValPair<int, bool>(doorNum, true));
    }
    //checks to see if doorNum is in list, if not, door will not be opened when scene loads
    public bool getDoorStayOpen(int doorNum)
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

    #endregion
}
