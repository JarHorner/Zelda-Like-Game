using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{

    #region Variables
    //variable can be changed.
    [SerializeField] private int damageDealt = 1;
    [SerializeField] ParticleSystem damageBurst;
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
            //spawns particles when hitting enemy
            ParticleSystem partSys = Instantiate(damageBurst, other.transform.position, other.transform.rotation);
            partSys.Play(true);
            eHealthMan.DamageEnemy(damageDealt, this.transform);
        }
    }



    #endregion
}
