using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    #region Variables
    private GameObject playerObj;
    public float speed;
    #endregion

    #region Methods

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerObj = col.gameObject;
            playerObj.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerObj = col.gameObject;
            playerObj.GetComponent<Rigidbody2D>().velocity = Vector3.right * speed;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerObj = col.gameObject;
            playerObj.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }
    #endregion

}
