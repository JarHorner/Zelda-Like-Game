using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set at the beginning of room ajacent to a room with falling floors to reset each object.
public class ResetFloors : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject go;
    private List<FallingFloor> resetableObjects = new List<FallingFloor>();

    #endregion

    #region Methods

    void Start() 
    {
        //adds each object into a list to easily reset.
        for (int i = 0; i < go.transform.childCount; i++)
        {
            resetableObjects.Add(go.transform.GetChild(i).GetComponent<FallingFloor>());
        }
    }

    //if player is in collider, each object in list is enabled and set to default sprite.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            foreach (FallingFloor floor in resetableObjects)
            {
                floor.GetComponent<Pitfall>().enabled = false;
                floor.GetComponent<Animator>().SetBool("SteppedOn", false);
            }
            Debug.Log("Room Reset!");
        }
    }

    #endregion

}
