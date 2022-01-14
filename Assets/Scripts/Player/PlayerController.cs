using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum PlayerState 
{
    walk,
    swim,
    attack,
    interact,
    dead
}

public class PlayerController : MonoBehaviour
{
    #region Variables
    public PlayerState currentState;
    //used to keep track of the players last location
    private static Vector2 lastPlayerLocation;
    public float moveSpeed;
    private float attackCounter = 0.25f;
    private bool isMoving = false;
    private bool isSwimming = false;
    private bool isCarrying = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    public string startPoint;
    [SerializeField] private  AudioSource swingSound;
    [SerializeField] private  AudioSource walkingSound;
    [SerializeField] private  AudioSource swimmingSound;
    [SerializeField] private AudioClip[] swingClips;
    private Vector2 movement;
    private static bool playerExists;
    private UIManager uiManager;
    private bool onConveyor = false;
    #endregion

    #region Unity Methods

    void Awake() 
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
        lastPlayerLocation = new Vector2(0, 0);
        Debug.Log(lastPlayerLocation);
    }

    //Singleton affect code
    void Start() 
    {
        if (!playerExists)
        {
            playerExists = true;
            //ensures same player object is not destoyed when loading new scences
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
        currentState = PlayerState.walk;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
    }

    void Update()
    {
        //gets input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack)
        {
           StartCoroutine(PlayerAttack());
        }
        else if (currentState == PlayerState.dead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //if player was swimming when died, this variable is true and when spawns again, wont be swimming.
            animator.SetBool("isDead", true);
            animator.SetBool("isSwimming", false);
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.swim)
        {
            PlayerMoving();
        }

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
    }

    //this is where the actual movement happens. better for performance, not tying movement to framerate.
    void FixedUpdate()
    {
        if (currentState == PlayerState.walk || currentState == PlayerState.swim)
        {
            //enables movement
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void PlayerMoving()
    {
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
    }

    //if not currently attacking, starts the animation, if attack button has been pressed again, another attack
    //animation is qued up. Creating smooth attacking if the attack button is spammed.
    private IEnumerator PlayerAttack()
    {
        currentState = PlayerState.attack;
        animator.SetBool("isAttacking", true);
        //randomizes sounds used when attacking.
        swingSound.clip = swingClips[Random.Range(0, swingClips.Length)];
        swingSound.Play();

        yield return new WaitForSeconds(attackCounter);

        animator.SetBool("isAttacking", false);
        currentState = PlayerState.walk;
    }

    //enables regular movement when colliding with OutOfWater trigger when leaving water.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Water") 
        {
            //sets swimming animation and new player speed
            animator.SetBool("isSwimming", true);
            currentState = PlayerState.swim;
            isSwimming = true;
            moveSpeed = 4f;
        }
        else if (collider.gameObject.tag == "OutOfWater")
        {
            animator.SetBool("isSwimming", false);
            if (isSwimming)
                isSwimming = false;
            moveSpeed = 5f;
        }
    }

    //Getters and Setters for player variables.

   public Vector2 LastPlayerLocation
    {
        get { return lastPlayerLocation; }
        set { lastPlayerLocation = value; }
    }

    public Vector2 Movement
    {
        get { return movement; }
        set { movement = value; }
    }

    public Animator Animator
    {
        get { return animator; }
    }
    public string StartPoint
    {
        get { return startPoint; }
        set { startPoint = value; }
    }
    public bool IsCarrying
    {
        get { return isCarrying; }
        set { isCarrying = value; }
    }
    public bool OnConveyor
    {
        get { return onConveyor; }
        set { onConveyor = value; }
    }
    #endregion
}
