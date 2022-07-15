using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic structure of inventory, contains all items in a list.
[CreateAssetMenu(fileName = "New Travel Location", menuName = "World/Location")]
public class FastTravelLocation : ScriptableObject
{
    public string locationName;
    public Vector2 cameraMinBounds;
    public Vector2 cameraMaxBounds;
    public Vector2 coordinates;
}
