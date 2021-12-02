using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private HealthManager healthManager;
    
    //these three varibles can be adjusted at any time
    [SerializeField] private int damageDealt = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifespan = 3f;

    #endregion

    #region Methods

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = FindObjectOfType<HealthManager>();
        //adds impulse force at start so the speed stays the same.
        rb.AddForce(new Vector2(0.0f, -speed), ForceMode2D.Impulse);
    }
    void Update() 
    {
        //destroys projectile if havent touched anything within its lifespan.
        Destroy (gameObject, lifespan);
    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            healthManager.DamagePlayer(damageDealt);
            Destroy(gameObject);
        } 
        else if (other.gameObject.tag == "Object")
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
