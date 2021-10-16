using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    #region Variables
    [SerializeField] private int playerLevel = 1;
    [SerializeField] private int maxLevel = 10;
    [SerializeField] private TMP_Text menuLevelText;
    [SerializeField] private TMP_Text menuCurrentExpText;
    [SerializeField] private TMP_Text menuExpNextlevelText;
    [SerializeField] private int currentExp = 0;
    private int[] expToLevelUp;
    private int baseExp = 0;

    #endregion

    #region Unity Methods

    void Start()
    {
        //starts coroutine setting up exp values to level
        menuLevelText.text = "LEVEL:   " + playerLevel;
        expToLevelUp = new int[maxLevel];
        StartCoroutine(ExpToLevel());
    }

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
            //algorithm setting up exp to level (can always be changed)
            baseExp = Mathf.RoundToInt((baseExp + 100) * 1.1f);
            expToLevelUp[i] = baseExp;
            yield return expToLevelUp[i];
        }
    }

    //basic setter, but adds parameter
    public void SetCurrentExp(int addedExp)
    {
        currentExp += addedExp;
    }

    #endregion
}
