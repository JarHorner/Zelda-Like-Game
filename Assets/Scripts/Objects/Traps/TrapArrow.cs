using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private HealthManager healthManager;
    private GameObject spawn;
    //these three varibles can be adjusted at any time
    [SerializeField] private int damageDealt;
    [SerializeField] private float speed;
    [SerializeField] private float lifespan;

    #endregion

    #region Methods

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        healthManager = FindObjectOfType<HealthManager>();
        spawn = this.transform.parent.gameObject;
        //adds impulse force at start so the speed stays the same.
        ShootDirection();
    }
    void Update() 
    {
        //destroys projectile if havent touched anything within its lifespan.
        Destroy (gameObject, lifespan);
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "HitBox")
        {
            Debug.Log("Hit");
            healthManager.DamagePlayer(damageDealt);
            HealthVisual.healthSystemStatic.Damage(damageDealt);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Object")
        {
            Destroy(gameObject);
        }
    }

    //depending on the z rotation of the ArrowTrap, the arrow will shoot in that direction.
    public virtual void ShootDirection()
    {
        if (spawn.transform.parent.gameObject.transform.rotation.z == 0)
        {
            //moves down
            rb.AddForce(new Vector2(0.0f, -speed), ForceMode2D.Impulse);
        }
        else if (spawn.transform.parent.gameObject.transform.rotation.z == 1)
        {
            //moves up
            rb.AddForce(new Vector2(0.0f, speed), ForceMode2D.Impulse);
        }
        else if (spawn.transform.parent.gameObject.transform.rotation.z < 0)
        {
            //moves left
            rb.AddForce(new Vector2(-speed, -0.0f), ForceMode2D.Impulse);
        }
        else if(spawn.transform.parent.gameObject.transform.rotation.z > 0)
        {
            //moves right
            rb.AddForce(new Vector2(speed, -0.0f), ForceMode2D.Impulse);
        }
    }

    #endregion
}
