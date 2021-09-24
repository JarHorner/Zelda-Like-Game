using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBlocks : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject go;
    private List<MoveOnceBlock> resetableObjects = new List<MoveOnceBlock>();

    #endregion

    #region Methods

    void Start() 
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            resetableObjects.Add(go.transform.GetChild(i).GetComponent<MoveOnceBlock>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            foreach (MoveOnceBlock block in resetableObjects)
            {
                block.transform.position = new Vector3(block.StartX, block.StartY, 0);
                block.NotMoved = true;
                block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            Debug.Log("Room Reset!");
        }
    }

    #endregion

}
