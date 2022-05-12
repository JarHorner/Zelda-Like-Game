using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    #region Variables
    private bool canMove;
    [SerializeField] private GameObject parent;
    [SerializeField] private char direction;

    #endregion

    #region Method
    private void Start() 
    {
        parent.transform.parent = null;
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
            canMove = false;
        }
    }

    public bool CanMove 
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public char Direction 
    {
        get { return direction; }
    }

    #endregion
}
