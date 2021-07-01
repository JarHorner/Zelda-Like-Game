using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    #region Variables
    public int playerLevel = 1;
    public int maxLevel = 10;
    public Text levelText;
    public Text menuLevelText;
    public Text menuCurrentExpText;
    public Text menuExpNextlevelText;
    public int currentExp;
    public int[] expToLevelUp;
    private int baseExp = 0;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        //starts coroutine setting up exp values to level
        levelText.text = "Level:   " + playerLevel;
        expToLevelUp = new int[maxLevel];
        StartCoroutine(ExpToLevel());
    }

    // Update is called once per frame
    void Update()
    {
        //if exp is enough to level up, UI changes happen
        if (currentExp >= expToLevelUp[playerLevel])
        {
            playerLevel++;
            currentExp = 0;
            levelText.text = "Level:   " + playerLevel;
            menuLevelText.text = levelText.text;
        }
    }

    void LateUpdate()
    {
        //changed exp values after every frame
        menuCurrentExpText.text = "Current Exp:    " + currentExp;
        menuExpNextlevelText.text = "Exp to next level:    " + Mathf.Abs(currentExp - expToLevelUp[playerLevel]);
    }

    private IEnumerator ExpToLevel() 
    {
        for (int i = 1;i < maxLevel; i++)
        {
            baseExp = Mathf.RoundToInt((baseExp + 100) * 1.1f);
            expToLevelUp[i] = baseExp
    ;
            //Debug.Log("Exp at Level " + (i + 1) + " is " + expToLevelUp[i]);
            yield return expToLevelUp[i];
        }
    }

    #endregion
}
