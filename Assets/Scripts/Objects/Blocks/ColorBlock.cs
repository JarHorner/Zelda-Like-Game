using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlock : MonoBehaviour
{
    #region Variable

    [SerializeField] private char color;
    [SerializeField] private Animator animator;

    #endregion

    #region Methods

    public char Color
    {
        get { return color; }
    }

    public Animator Animator
    {
        get { return animator; }
    }

    #endregion
}
