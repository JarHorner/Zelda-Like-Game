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
    private bool isWalking = false;
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
    private Collider2D capsule;
    private UIManager uiManager;

    #endregion

    #region Unity Methods

    void Awake() {
        uiManager = GameObject.FindObjectOfType<UIManager>();
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
            //sets new directions from the movement and sets bool, to play walking sound
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            isWalking = true;
        } 
        else
        {
            isWalking = false;
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (isSwimming && isWalking)
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
        if (isWalking)
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
            swingSound.clip = swingClips[Random.Range(0, swingClips.Length)];
            swingSound.Play();
            isAttacking = true;
        }
    }

    //not stuck to framerate
    void FixedUpdate()
    {
        //enables movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    //enables swimming in the water, grabbing a key
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "OutOfWater")
        {
            animator.SetBool("isSwimming", false);
            if (isSwimming)
                isSwimming = false;
            moveSpeed = 5f;
        }
        else if (collider.gameObject.tag == "Key") 
        {
            //will need to change with more dungeons
            uiManager.addKey(Dungeon1Manager.getDungeonName());
            Destroy(collider.gameObject);
        }
        else if (collider.gameObject.tag == "DungeonKey") 
        {
            Debug.Log(collider.name);
            uiManager.activateDungeonKey(collider.name);
            Destroy(collider.gameObject);
        }
    }

    public string getStartPoint()
    {
        return startPoint;
    }

    public void setStartPoint(string newStartPoint)
    {
        startPoint = newStartPoint;
    }
    public void setPlayerWalking(bool status)
    {
        isWalking = status;
    }
    public void setPlayerSwimming(bool status)
    {
        isSwimming = status;
    }
    #endregion
}
