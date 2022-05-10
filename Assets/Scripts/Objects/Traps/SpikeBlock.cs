using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    #region Variables

    [SerializeField] private Animator animator;
    private float min;
    private float max;
    [SerializeField] private float distance;
    [SerializeField] private float speed;

    [Tooltip("true is x axis, false is y axis")]
    [SerializeField] private bool direction;

    #endregion

    #region Methods
    //sets min and max distance depending on direction
    void Start () 
    {
        if (direction)
        {
            min = transform.position.x;
            max = transform.position.x + distance;
        }
        else
        {
            min = transform.position.y;
            max = transform.position.y + distance;
        }
   
    }

    //uses the math function PingPong to bounce the object back and forth from its min and max position
    void Update () 
    {
        if (direction)
            transform.position = new Vector3(Mathf.PingPong(Time.time*speed,max-min)+min, transform.position.y, transform.position.z);
        else 
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time*speed,max-min)+min, transform.position.z);
    }

    #endregion
}
