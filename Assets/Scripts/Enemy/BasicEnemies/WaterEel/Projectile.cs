using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    [SerializeField] private AudioSource movingSound;
    private PlayerController player;
    private Vector3 playerPosition;

    //these three varibles can be adjusted at any time
    [SerializeField] private FloatValue damageDealt;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifespan = 3f;
    #endregion

    #region Unity Methods
    void Awake() 
    {
        rb= GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        //finds players current position
        playerPosition = player.transform.position;
                
                
        //scale the movement on each axis by the directionOfTravel vector components
        Vector2 force = (playerPosition - transform.position).normalized;
        rb.AddForce(force * speed, ForceMode2D.Impulse);
    }

    void Start() {
        movingSound.Play();
    }

    //ensures projectile when spawned goes the same speed towards and past the players location when instatiated.
    void Update() {
        //destroys projectile if havent touched anything within its lifespan.
        Destroy (gameObject, lifespan);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "HitBox")
        {
            Debug.Log("Hit");
            player.DamagePlayer(damageDealt.InitalValue);
            Destroy(gameObject);
        } 
        else if (other.gameObject.tag == "Object")
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
