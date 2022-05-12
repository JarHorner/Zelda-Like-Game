using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSenseSpikeBlock : Enemy
{
    #region Variables

    [SerializeField] private TriggerArea[] triggerArea;
    private Vector3 min;
    private Vector3 max;
    private bool canMove;
    private bool reset = true;
    [SerializeField] private float distance;
    [SerializeField] private float speedBack;
    #endregion

    #region Methods
    //sets min distance
    void Start () 
    {
        min = transform.position;
    }

    //checks each trigger area to see if player has triggered it. if so, the block will move in that direction.
    void Update() 
    {
            foreach (var area in triggerArea)
            {
                if (!reset)
                    break;
                if (area.CanMove && reset)
                {
                    canMove = area.CanMove;
                    reset = false;
                    DetermineDirection(area);
                }
            }
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, max, Time.deltaTime * MoveSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, min, Time.deltaTime * speedBack);
                if (transform.position == min) 
                {
                    reset = true;
                }
                
            }
    }

    //determines the direction the block will move based on char of TriggerArea.
    private void DetermineDirection(TriggerArea area)
    {
        switch (area.Direction)
        {
            case 'N':
                max = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
                break;
            case 'E':
                max = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z); 
                break;
            case 'S':  
                max = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);
                break;
            case 'W':
                max = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z); 
                break;
           default:
                Debug.Log("Incorrect Direction");
                break;
        }
    }

    //if the block collides with select gameobjects, it "bounces" back
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "MovableBlock" || other.gameObject.tag == "Walls" || other.gameObject.tag == "Object")
        {
            if (canMove)
            {
                canMove = false;
                foreach (var area in triggerArea)
                {
                    if (area.CanMove)
                    {
                        area.CanMove = false;
                    }
                }
            }
            else
            {
                canMove = true;
                foreach (var area in triggerArea)
                {
                    if (area.CanMove)
                    {
                        area.CanMove = true;
                    }
                }
            }
        }
    }


    #endregion
}
