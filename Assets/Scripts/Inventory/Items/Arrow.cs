using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    //these three varibles can be adjusted at any time
    [SerializeField] private int damageDealt;
    [SerializeField] private float speed;
    [SerializeField] private float lifespan;

    #endregion

    #region Methods

    void Update() 
    {
        //destroys projectile if havent touched anything within its lifespan.
        Destroy (gameObject, lifespan);
    }

    //does damamge to enemy, and connects with obejcts and walls.
    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.layer == 10) //layer 10 is Walls
        {
            StartCoroutine(ArrowHitWall());
        }
        else if (other.gameObject.tag == "Object")
        {
            StartCoroutine(ArrowHitObject());
        }
        else if (other.tag == "Enemy" || other.tag == "Boss")
        {
            EnemyHealthManager eHealthMan = other.gameObject.GetComponent<EnemyHealthManager>();
            foreach (var weakness in eHealthMan.weaknesses.itemsWeakTo)
            {
                Debug.Log("cycle");
                if (weakness == "Arrow")
                {
                    Debug.Log("Hit");
                    if (other.gameObject.tag == "Enemy")
                        eHealthMan.DamageEnemy(damageDealt, this.transform);
                    else
                        eHealthMan.DamageBoss(damageDealt, this.transform);
                    Destroy(gameObject);
                    break;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    //applies a force and ensure sprite is facing the correct roation.
    public void Setup(Vector2 velocity, Vector3 direction)
    {
        //rb.velocity = velocity.normalized * speed;
        rb.AddForce(velocity * speed, ForceMode2D.Impulse);
        this.transform.rotation = Quaternion.Euler(direction);
    }

    //2 different IEnumerators because it hits a wall and object alittle differently.

    private IEnumerator ArrowHitWall()
    {
        Debug.Log("Hit Wall");
        yield return new WaitForSeconds(0.13f);
        animator.SetBool("HitObject", true);
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private IEnumerator ArrowHitObject()
    {
        Debug.Log("Hit Object");
        yield return new WaitForSeconds(0.07f);
        animator.SetBool("HitObject", true);
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    #endregion
}
