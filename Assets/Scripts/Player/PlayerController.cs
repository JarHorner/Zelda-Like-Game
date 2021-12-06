using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public float moveSpeed;
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool doubleUpAttack;
    private bool isAttacking = false;
    private bool isMoving = false;
    private bool isSwimming = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    public string startPoint;
    [SerializeField] private  AudioSource swingSound;
    [SerializeField] private  AudioSource walkingSound;
    [SerializeField] private  AudioSource swimmingSound;
    [SerializeField] private AudioClip[] swingClips;
    private Vector2 movement;
    private static bool playerExists;
    private Collider2D hitBox;
    private UIManager uiManager;
    private bool onConveyor = false;
    #endregion

    #region Unity Methods

    void Awake() 
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }

    //Singleton affect code
    void Start() 
    {
        if (!playerExists)
        {
            playerExists = true;
            hitBox = this.GetComponent<CapsuleCollider2D>();
            //ensures same player object is not destoyed when loading new scences
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
    }

    void Update()
    {
        //enables collider after being disable while loading new scene
        //used to enemies dont push player while loading 
        if (hitBox.enabled == false)
        {
            hitBox.enabled = true;
        }

        //gets input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            //sets new directions from the movement and sets bool, to play walking sound
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            isMoving = true;
        } 
        else
        {
            isMoving = false;
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //plays swimming sound if in water and moving, does not if already playing
        if (isSwimming && isMoving)
        {
            if (!swimmingSound.isPlaying)
            {
                swimmingSound.Play();
            }
        }
        else
        {
            swimmingSound.Stop();
        }

        //plays walking sound, does not if already playing
        if (isMoving)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            walkingSound.Stop();
        }

        //if not currently attacking, starts the animation, if attack button has been pressed again, another attack
        //animation is qued up. Creating smooth attacking if the attack button is spammed.
        if (!isAttacking)
        {
            if (Input.GetButtonDown("Attack") || doubleUpAttack)
            {
                isAttacking = true;
                doubleUpAttack = false;
                attackCounter = attackTime;
                animator.SetBool("isAttacking", true);
                //randomizes sounds used when attacking.
                swingSound.clip = swingClips[Random.Range(0, swingClips.Length)];
                swingSound.Play();
            }
        }
        else if (Input.GetButtonDown("Attack"))
        {
            doubleUpAttack = true;
        }
        
        //attacking stops movement, carries out the animation, then reverts back
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
    }

    //this is where the actual movement happens. better for performance, not tying movement to framerate.
    void FixedUpdate()
    {
        //enables movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    //enables regular movement when colliding with OutOfWater trigger when leaving water.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "OutOfWater")
        {
            animator.SetBool("isSwimming", false);
            if (isSwimming)
                isSwimming = false;
            moveSpeed = 5f;
        }
    }

    //Getters and Setters for player variables.

    public Vector2 Movement
    {
        set { movement = value; }
    }
    public string StartPoint
    {
        get { return startPoint; }
        set { startPoint = value; }
    }
    public bool PlayerMoving
    {
        set { isMoving = value; }
    }
    public bool PlayerSwimming
    {
        set { isSwimming = value; }
    }
    public bool OnConveyor
    {
        get { return onConveyor; }
        set { onConveyor = value; }
    }
    #endregion
}
