using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : Enemy
{
    #region Variables

    [SerializeField] private Animator animator;
    private Vector3 min;
    private Vector3 max;
    private bool forward = true;
    [SerializeField] private float distance;

    [Tooltip("true is x axis, false is y axis")]
    [SerializeField] private char direction;

    #endregion

    #region Methods
    //sets min and max distance depending on direction
    void Start () 
    {
        min = transform.position;
        switch (direction)
        {
            case 'N':
                min = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                max = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
                break;
            case 'E':
                min = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z); 
                max = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z); 
                break;
            case 'S':
                min = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                max = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);
                break;
            case 'W':
                min = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z); 
                max = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z); 
                break;
           default:
                Debug.Log("Incorrect Direction");
                break;
        }
    }

    //uses MoveTowards to bounce the object back and forth from its min and max position
    void Update () 
    {
        if (forward)
        {
            transform.position = Vector3.MoveTowards(transform.position, max, Time.deltaTime * MoveSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, min, Time.deltaTime * MoveSpeed);
        }
    }

    //if the block collides with select gameobjects, it "bounces"
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "MovableBlock" || other.gameObject.tag == "Walls" || other.gameObject.tag == "Object")
        {
            if (forward)
            {
                forward = false;
            }
            else
            {
                forward = true;
            }
        }
    }

    #endregion
}
