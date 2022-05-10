using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    #region Variables
    [SerializeField] private TrapArrow arrow;
    [SerializeField] private GameObject target;
    private bool shootAgain = false;
    private float shootAgainTime = 1f;

    #endregion

    #region Methods

    //spawns arrow when player enters firing range
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Instantiate(arrow, target.transform);
            shootAgain = true;
        }
    }

    //continues shooting if player is in range, currently on a 1 second cooldown
    private void Update() 
    {
        if (shootAgain)
        {
            shootAgainTime -= Time.deltaTime;
            if (shootAgainTime <= 0f)
            {
                Instantiate(arrow, target.transform);
                shootAgainTime = 1f;
            }
        }
    }

    //stops shooting if player is not in range
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player" && other.GetType() != typeof(BoxCollider2D))
        {
            shootAgain = false;
            shootAgainTime = 1f;
        }
    }
    
    #endregion
}
