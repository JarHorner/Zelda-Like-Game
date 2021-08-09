using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    private HealthManager healthManager;
    public int damageDealt = 2;
    public float speed = 1f;
    public float lifespan = 2f;
    private Rigidbody2D rb;
    #endregion

    #region Unity Methods
    void Awake() 
    {
        healthManager = FindObjectOfType<HealthManager>();
        rb= GetComponent<Rigidbody2D>();
    }

    void Update() {
        rb.velocity = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * speed;
        Destroy (gameObject, lifespan);
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            healthManager.HurtPlayer(damageDealt);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Object")
        {
            Destroy(gameObject);
        }
        //else if (other.gameObject.tag == "Enemy")
        //{
        //    Destroy(gameObject);
        //}
    }

    #endregion
}
