using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject
{
    [SerializeField] private int initalValue;

    public int InitalValue
    {
        get { return initalValue; }
    }
}
