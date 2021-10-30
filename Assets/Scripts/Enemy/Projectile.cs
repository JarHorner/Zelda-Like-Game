using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    private HealthManager healthManager;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource movingSound;
    private Vector3 playerPosition;
    private Vector3 currentPosition;

    //these three varibles can be adjusted at any time
    [SerializeField] private int damageDealt = 2;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifespan = 3f;
    #endregion

    #region Unity Methods
    void Awake() 
    {
        healthManager = FindObjectOfType<HealthManager>();
        rb= GetComponent<Rigidbody2D>();
        //finds players current position
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Start() {
        movingSound.Play();
    }

    //ensures projectile when spawned goes the same speed towards and past the players location when instatiated.
    void Update() {
        //scale the movement on each axis by the directionOfTravel vector components
        Debug.DrawLine(playerPosition, transform.position, Color.blue);
        Vector2 force = (playerPosition - transform.position).normalized;

        if(Vector3.Dot(rb.velocity, force) < 0)
            return;

        rb.AddForce(force * speed, ForceMode2D.Impulse);
        //destroys projectile if havent touched anything within its lifespan.
        Destroy (gameObject, lifespan);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
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
