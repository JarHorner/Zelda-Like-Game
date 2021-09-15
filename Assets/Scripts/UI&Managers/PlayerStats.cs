using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    #region Variables
    public int playerLevel = 1;
    public int maxLevel = 10;
    public TMP_Text menuLevelText;
    public TMP_Text menuCurrentExpText;
    public TMP_Text menuExpNextlevelText;
    public int currentExp = 0;
    public int[] expToLevelUp;
    private int baseExp = 0;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        //starts coroutine setting up exp values to level
        menuLevelText.text = "LEVEL:   " + playerLevel;
        expToLevelUp = new int[maxLevel];
        StartCoroutine(ExpToLevel());
    }

    // Update is called once per frame
    void Update()
    {
        //if exp is enough to level up, UI changes happen
        if (currentExp >= expToLevelUp[playerLevel])
        {
            playerLevel += 1;
            currentExp = 0;
            menuLevelText.text = "LEVEL:   " + playerLevel;
        }
    }

    void LateUpdate()
    {
        //changed exp values after every frame
        menuCurrentExpText.text = "CURRENT EXP:    " + currentExp;
        menuExpNextlevelText.text = "EXP TO NEXT LEVEL:    " + Mathf.Abs(currentExp - expToLevelUp[playerLevel]);
    }

    private IEnumerator ExpToLevel() 
    {
        for (int i = 1;i < maxLevel; i++)
        {
            baseExp = Mathf.RoundToInt((baseExp + 100) * 1.1f);
            expToLevelUp[i] = baseExp;
            yield return expToLevelUp[i];
        }
    }

    #endregion
}
