using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float moveSpeed;
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool isAttacking = false;
    public Rigidbody2D rb;
    public Animator animator;
    public string startPoint;
    private Vector2 movement;
    private static bool playerExists;
    private Collider2D capsule;
    private UIManager uiManager;

    #endregion

    #region Unity Methods

    void Awake() {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        Debug.Log($"found {uiManager}");
    }

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
        //used to enemies dont push player while loading 
        if (capsule.enabled == false)
        {
            capsule.enabled = true;
        }

        //gets input
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
        //enables movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    //enables swimming in the water
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "OutOfWater")
        {
            animator.SetBool("isSwimming", false);
            moveSpeed = 5f;
        }
        else if (collider.gameObject.tag == "Key") 
        {
            uiManager.addKey();
            Destroy(collider.gameObject);
        }
    }
    
    #endregion
}
