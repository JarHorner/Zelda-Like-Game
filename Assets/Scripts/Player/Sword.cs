using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    #region Variables
    //variable can be changed.
    [SerializeField] private int damageDealt = 1;

    #endregion

    #region Unity Methods

    //if collider touches an enemy, it is deactivated to not "hit" multiple times, then deals damage towards the enemy hit.
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy") 
        {
            this.gameObject.SetActive(false);
            EnemyHealthManager eHealthMan;
            eHealthMan = other.gameObject.GetComponent<EnemyHealthManager>();
            Debug.Log("Enemy Hit!");
            eHealthMan.HurtEnemy(damageDealt);
            //pushes enemy back if not on water layer (4)
            if (other.gameObject.layer != 4)
            {
                Vector2 difference = other.transform.position - transform.position;
                other.transform.position = new Vector2(other.transform.position.x + difference.x, other.transform.position.y + difference.y);
            }
        }
    }

    #endregion
}
