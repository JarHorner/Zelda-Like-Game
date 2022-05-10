using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlockSwitch : MonoBehaviour
{
    #region Variables

    private ColorBlock[] allColorBlocks;
    private static char color ='R';
    [SerializeField] private Animator animator;

    #endregion

    #region Methods
    void Start() 
    {
        allColorBlocks = FindObjectsOfType<ColorBlock>();

        foreach (ColorBlock block in allColorBlocks)
        {
            if (block.Color != color)
            {
                block.Animator.SetBool("SwitchPressed", true);
            }
            else
            {
                block.Animator.SetBool("SwitchPressed", false);
            }
        }
    }

    public void SwapColor()
    {
        Debug.Log("Switch hit");
        if (animator.GetBool("Red") == true)
        {
            animator.SetBool("Red", false);
            animator.SetBool("Green", true);
            color = 'G';
        }
        else if (animator.GetBool("Green") == true)
        {
            animator.SetBool("Green", false);
            animator.SetBool("Red", true);
            color = 'R';
        }
        
        foreach (ColorBlock block in allColorBlocks)
        {
            if (block.Color != color)
            {
                block.Animator.SetBool("SwitchPressed", true);
            }
            else
            {
                block.Animator.SetBool("SwitchPressed", false);
            }
        }
    }
    
    #endregion

}
