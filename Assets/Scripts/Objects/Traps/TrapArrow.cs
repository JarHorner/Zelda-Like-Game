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
    [SerializeField] private int damageDealt = 1;
    [SerializeField] private float speed = 10f;
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            healthManager.DamagePlayer(damageDealt);
            Destroy(gameObject);
        }
    }

    //depending on the z rotation of the ArrowTrap, the arrow will shoot in that direction.
    private void ShootDirection()
    {
        if (spawn.transform.parent.gameObject.transform.rotation.z == 0)
        {
            rb.AddForce(new Vector2(0.0f, -speed), ForceMode2D.Impulse);
        }
        else if (spawn.transform.parent.gameObject.transform.rotation.z == 1)
        {
            rb.AddForce(new Vector2(0.0f, speed), ForceMode2D.Impulse);
        }
        else if (spawn.transform.parent.gameObject.transform.rotation.z < 0)
        {
            rb.AddForce(new Vector2(-speed, -0.0f), ForceMode2D.Impulse);
        }
        else if(spawn.transform.parent.gameObject.transform.rotation.z > 0)
        {
            rb.AddForce(new Vector2(speed, -0.0f), ForceMode2D.Impulse);
        }
    }

    #endregion
}
