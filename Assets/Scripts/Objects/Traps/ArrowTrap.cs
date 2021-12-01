using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    #region Variables
    [SerializeField] TrapArrow arrow;
    [SerializeField] GameObject target;

    #endregion

    #region Methods

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && other.GetType() != typeof(BoxCollider2D))
        {
            Instantiate(arrow, target.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.tag == "Player" && other.GetType() != typeof(BoxCollider2D))
        {
            Instantiate(arrow, target.transform);
        }
    }

    #endregion
}
