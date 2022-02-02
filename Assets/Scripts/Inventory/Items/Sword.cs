using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    #region Variables
    //variable can be changed.
    [SerializeField] private int damageDealt = 1;
    [SerializeField] ParticleSystem damageBurst;
    [SerializeField] Knockback knockback;
    #endregion

    #region Unity Methods

    //if collider touches an enemy, it is deactivated to not "hit" multiple times, then deals damage towards the enemy hit.
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            EnemyHealthManager eHealthMan = other.gameObject.GetComponent<EnemyHealthManager>();
            foreach (var weakness in eHealthMan.weaknesses.itemsWeakTo)
            {
                if (weakness == "Sword")
                {
                    //spawns particles when hitting enemy
                    ParticleSystem partSys = Instantiate(damageBurst, other.transform.position, other.transform.rotation);
                    partSys.Play(true);
                    if (other.gameObject.tag == "Enemy")
                    {
                        eHealthMan.DamageEnemy(damageDealt, this.transform);
                        if (other.gameObject.activeSelf == true)
                        {
                            knockback.PushBack(this.transform, other.GetComponent<Rigidbody2D>());
                        }
                    }
                    else
                    {
                        eHealthMan.DamageBoss(damageDealt, this.transform);
                    }
                    break;
                }
            }
        }
    }

    #endregion
}
