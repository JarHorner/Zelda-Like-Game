using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool isAttacking = false;
    public Rigidbody2D rb;
    public Animator animator;
    public string startPoint;
    private Vector2 movement;
    private static bool playerExists;
    private Collider2D capsule;

    void Start() {
        if (!playerExists)
        {
            playerExists = true;
            capsule = this.GetComponent<CapsuleCollider2D>();
            //ensures same player object is not destoyed when loading new scences
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //enables collider after being disable while loading new scene
        if (capsule.enabled == false)
        {
            capsule.enabled = true;
        }

        //input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //if attacking stops movement, carries out the animation, then reverts back
        if (isAttacking)
        {
            movement = Vector2.zero;
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                animator.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }

        //attack animation is started when pressing R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            attackCounter = attackTime;
            animator.SetBool("isAttacking", true);
            isAttacking = true;
        }
    }

    //not stuck to framerate
    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Water") 
        {
            animator.SetBool("isSwimming", true);
            moveSpeed = 4f;
        }
        else if (collider.gameObject.tag == "OutOfWater")
        {
            animator.SetBool("isSwimming", false);
            moveSpeed = 6f;
        }
    }
}
