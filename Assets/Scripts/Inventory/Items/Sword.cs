using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
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
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            EnemyHealthManager eHealthMan = other.gameObject.GetComponent<EnemyHealthManager>();
            foreach (var weakness in eHealthMan.weaknesses.itemsWeakTo)
            {
                if (weakness == "Sword")
                {
                    //this.gameObject.SetActive(false);
                    //spawns particles when hitting enemy
                    ParticleSystem partSys = Instantiate(damageBurst, other.transform.position, other.transform.rotation);
                    partSys.Play(true);
                    if (other.gameObject.tag == "Enemy")
                    {
                        eHealthMan.DamageEnemy(damageDealt, this.transform);
                        Debug.Log("Push Enemy");
                        PushBack(this.transform, other.GetComponent<Rigidbody2D>(), eHealthMan);
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

    private void PushBack(Transform weaponTrans, Rigidbody2D enemyRb, EnemyHealthManager healthManager)
    {
        Debug.Log("Pushed");
        Vector2 difference = enemyRb.transform.position - weaponTrans.position;
        difference = difference.normalized * 2f;
        enemyRb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockCo(enemyRb));
    }

    private IEnumerator KnockCo(Rigidbody2D enemyRb)
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Velocity to Zero");
        enemyRb.velocity = Vector2.zero;
    }


    #endregion
}
