using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weakness", menuName = "Enemies/Weaknesses")]
public class Weakness : ScriptableObject
{
    public List<string> itemsWeakTo = new List<string>();
}
