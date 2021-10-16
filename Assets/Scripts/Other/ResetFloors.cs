using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFloors : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject go;
    private List<FallingFloor> resetableObjects = new List<FallingFloor>();

    #endregion

    #region Methods

    void Start() 
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            resetableObjects.Add(go.transform.GetChild(i).GetComponent<FallingFloor>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            foreach (FallingFloor floor in resetableObjects)
            {
                floor.GetComponent<Pitfall>().enabled = false;
                floor.GetComponent<Animator>().SetBool("SteppedOn", false);
                // if (!floor.gameObject.activeSelf)
                // {
                //     Debug.Log("not active");
                //     floor.gameObject.SetActive(true);
                // }
            }
            Debug.Log("Room Reset!");
        }
    }

    #endregion

}
