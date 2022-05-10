using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    #region Variables
    private bool canMove;

    #endregion

    #region Method
    private void Start() 
    {
        transform.parent = null;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            canMove = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            //canMove = false;
        }
    }

    public bool CanMove 
    {
        get { return canMove; }
        set { canMove = value; }
    }

    #endregion
}
