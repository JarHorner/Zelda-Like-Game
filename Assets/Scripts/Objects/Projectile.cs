using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    private HealthManager healthManager;
    public int damageDealt = 2;
    public float speed = 10f;
    public float lifespan = 3f;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource movingSound;
    private Vector3 playerPosition;
    private Vector3 currentPosition;
    #endregion

    #region Unity Methods
    void Awake() 
    {
        healthManager = FindObjectOfType<HealthManager>();
        rb= GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        //playerPosition.y += 0.5f;
    }
    void Start() {
        movingSound.Play();
    }

    void Update() {
        //scale the movement on each axis by the directionOfTravel vector components
        Debug.DrawLine(playerPosition, transform.position, Color.blue);
        Vector3 force = (playerPosition - transform.position).normalized;

        if(Vector3.Dot(rb.velocity, force) < 0)
            return;

        rb.AddForce(force * speed, ForceMode2D.Impulse);

        //rb.velocity = force * speed;

        //rb.AddForce(force, ForceMode2D.Impulse);
        //transform.Translate(Vector3.left * (speed * Time.deltaTime));
        //transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        Destroy (gameObject, lifespan);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
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
        // else if (other.gameObject.tag == "Enemy")
        // {
        //     Physics2D.IgnoreCollision(other.collider, this.GetComponent<CircleCollider2D>());
        // }
    }

    #endregion
}
