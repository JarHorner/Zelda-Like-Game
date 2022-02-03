using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set at the beginning of room ajacent to a room with moveable blocks to reset each object.
public class ResetBlocks : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject go;
    [SerializeField] private string typeOfBlock;
    private List<MoveOnceBlock> moveOnceBlocks = new List<MoveOnceBlock>();
    private List<FreeMovePushableBlock> freeMoveBlocks = new List<FreeMovePushableBlock>();

    #endregion

    #region Methods

    void Start() 
    {
        //adds each object into a list to easily reset.
        for (int i = 0; i < go.transform.childCount; i++)
        {
            if (typeOfBlock.Equals("MoveOnce"))
                moveOnceBlocks.Add(go.transform.GetChild(i).GetComponent<MoveOnceBlock>());
            else if (typeOfBlock.Equals("FreeMove"))
                freeMoveBlocks.Add(go.transform.GetChild(i).GetComponent<FreeMovePushableBlock>());
        }
    }

    //if player is in collider, each object in list is moved to its starting position, and bodytype reset.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.tag == "Player")
        {
            if (typeOfBlock.Equals("MoveOnce"))
            {
                foreach (MoveOnceBlock block in moveOnceBlocks)
                {
                    block.transform.position = new Vector3(block.StartX, block.StartY, 0);
                    block.NotMoved = true;
                    block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
            else if (typeOfBlock.Equals("FreeMove"))
            {
                foreach (FreeMovePushableBlock block in freeMoveBlocks)
                {
                    block.transform.position = new Vector3(block.StartX, block.StartY, 0);
                    block.NotMoved = true;
                    block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
            Debug.Log("Room Reset!");
        }
    }

    #endregion

}
