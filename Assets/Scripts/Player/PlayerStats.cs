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
    [SerializeField] private TMP_Text menuLevelNum;
    [SerializeField] private TMP_Text menuCurrentExpNum;
    [SerializeField] private TMP_Text menuExpNextLevelNum;
    [SerializeField] private int currentExp = 0;
    private int[] expToLevelUp;
    private int baseExp = 0;

    #endregion

    #region Unity Methods

    void Start()
    {
        //starts coroutine setting up exp values to level
        menuLevelNum.text = playerLevel.ToString();
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
            menuLevelNum.text = playerLevel.ToString();
        }
    }

    void LateUpdate()
    {
        //changed exp values after every frame
        menuCurrentExpNum.text = currentExp.ToString();
        menuExpNextLevelNum.text = Mathf.Abs(currentExp - expToLevelUp[playerLevel]).ToString();
    }

    private IEnumerator ExpToLevel() 
    {
        //algorithm setting up exp to level (can always be changed)
        for (int i = 1;i < maxLevel; i++)
        {
            baseExp = Mathf.RoundToInt((baseExp + 100) * 1.1f);
            expToLevelUp[i] = baseExp;
            yield return expToLevelUp[i];
        }
    }

    //basic setter
    public void SetCurrentExp(int addedExp)
    {
        currentExp += addedExp;
    }

    #endregion
}
